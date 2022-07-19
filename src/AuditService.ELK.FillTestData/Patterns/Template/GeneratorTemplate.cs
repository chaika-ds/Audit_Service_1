using AuditService.ELK.FillTestData.Models;
using AuditService.ELK.FillTestData.Resources;
using Nest;
using Newtonsoft.Json;

namespace AuditService.ELK.FillTestData.Patterns.Template;

/// <summary>
///    Abstract Template model for Generators model
/// </summary>
internal abstract class GeneratorTemplate<TDtoModel>
    where TDtoModel : class
{
    private readonly IElasticClient _elasticClient;
    private string? _channelName;

    /// <summary>
    ///  Initialize Generator Template
    /// </summary>
    protected GeneratorTemplate(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;

        Task.Run(async () =>
        {
            var config = JsonConvert.DeserializeObject<BaseModel>(System.Text.Encoding.Default.GetString(ElcJsonResource.elkFillData));
            await CleanBeforeAsync(config);
            await GetAndCheckIndexAsync();
            await InsertAsync(config);
        });
    }

    /// <summary>
    ///    Abstract method for getting channel name
    /// </summary>
    protected abstract string? GetChanelName();
    
    /// <summary>
    ///    Get Identifier id for channel
    /// </summary>
    protected abstract string? GetIdentifierName();
    
    
    /// <summary>
    ///    Abstract method for getting resource data
    /// </summary>
    protected abstract Task<TDtoModel> CreateNewDtoAsync(ConfigurationModel configurationModel);
    
    /// <summary>
    ///    Abstract method for cleaning data
    /// </summary>
    /// <param name="config">Configuration model</param>
    private async Task CleanBeforeAsync(BaseModel? config)
    {
        _channelName = GetChanelName();

        config ??= new BaseModel {CleanBefore = true};

        var cleanBefore = config!.CleanBefore;

        if (cleanBefore)
        {
            Console.WriteLine(@"Start force clean data");

            await _elasticClient.DeleteByQueryAsync<TDtoModel>(w =>
                w.Query(x => x.QueryString(q
                    => q.Query("*"))).Index(_channelName));

            await _elasticClient.Indices.DeleteAsync(_channelName);

            Console.WriteLine(@"Force clean has been comlpete!");
        }
    }

    /// <summary>
    ///    Abstract method for checking index
    /// </summary>
    private async Task GetAndCheckIndexAsync()
    {
        var index = await _elasticClient.Indices.ExistsAsync(_channelName);

        if (!index.Exists)
        {
            Console.WriteLine($@"Creating index {_channelName}");

            var response = await _elasticClient.Indices.CreateAsync(_channelName, r
                => r.Map<TDtoModel>(x => x.AutoMap()));

            if (!response.ShardsAcknowledged)
                throw response.OriginalException;

            Console.WriteLine(@"Index successfully created!");
        }
    }
    
    /// <summary>
    ///     Override InsertAsync with you logic
    /// </summary>
    /// <param name="config">Configuration model</param>
    private async Task InsertAsync(BaseModel? config)
    {
        Console.WriteLine(@"Get configuration for generation data");

        var configurationModels = config!.Fillers;

        foreach (var configurationModel in configurationModels)
        {
            Console.WriteLine("");
            Console.WriteLine(@"Configuration model:");
            Console.WriteLine(JsonConvert.SerializeObject(configurationModel, Formatting.Indented));

            var data = GenerateDataAsync(configurationModel);

            Console.WriteLine($@"Generation {configurationModel.ServiceName} is completed");

            var identifier = GetIdentifierName();
            
            if (identifier == null)  throw new ArgumentNullException(GetIdentifierName(),@"Identifier Name can not be null");
    
            await foreach (var dto in data)
            {
                var identifierValue =  dto.GetType().GetProperty(identifier)?.GetValue(dto, null);

                if (identifierValue != null)
                    await _elasticClient.CreateAsync(dto, s => s.Index(GetChanelName()).Id(identifierValue.ToString()));

            }

            Console.WriteLine(@"Data has been saved");
            Console.WriteLine("");
        }

        Console.WriteLine("");
        Console.WriteLine(@"All configuration models has been saved");

        Console.WriteLine($@"Total records: {configurationModels.Sum(w => w.Count)}.");

        await Task.Delay(TimeSpan.FromMinutes(1));
        Environment.Exit(1);
    }
    
    /// <summary>
    ///     Data generation
    /// </summary>
    /// <param name="configurationModel">Configuration model</param>
    private async IAsyncEnumerable<TDtoModel> GenerateDataAsync(ConfigurationModel configurationModel)
    {
        for (var i = 0; i < configurationModel.Count; i++)
            yield return await CreateNewDtoAsync(configurationModel);
    }
}