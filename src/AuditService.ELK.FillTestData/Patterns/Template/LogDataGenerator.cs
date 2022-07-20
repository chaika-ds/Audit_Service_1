using AuditService.ELK.FillTestData.Models;
using AuditService.ELK.FillTestData.Resources;
using Nest;
using Newtonsoft.Json;

namespace AuditService.ELK.FillTestData.Patterns.Template;

/// <summary>
///    Log Data Generator model
/// </summary>
internal abstract class LogDataGenerator<TDtoModel>
    where TDtoModel : class
{
    private readonly IElasticClient _elasticClient;
    private readonly string? _indexName;
    private readonly string? _identifierName;

    /// <summary>
    ///  Initialize LogDataGenerator
    /// </summary>
    protected LogDataGenerator(IElasticClient elasticClient, string? indexName, string? identifierName)
    {
        _elasticClient = elasticClient;
        _indexName = indexName;
        _identifierName = identifierName;
    }

    /// <summary>
    ///    Execute template method
    /// </summary>
    public async Task GenerateAsync()
    {
        var config = JsonConvert.DeserializeObject<BaseModel>(System.Text.Encoding.Default.GetString(ElcJsonResource.elkFillData));
        
        await CleanBeforeAsync(config); 
        
        var index = await GetIndexAsync();
        
        await CheckIndexAsync(index);
        
        await InsertAsync(config);
    }

    /// <summary>
    ///    Create new Dto model
    /// </summary>
    protected abstract Task<TDtoModel> CreateNewDtoAsync(ConfigurationModel? configurationModel);
    
    
    /// <summary>
    ///     Cleaning data from elastic
    /// </summary>
    /// <param name="config">Configuration model</param>
    private async Task CleanBeforeAsync(BaseModel? config)
    {
        config ??= new BaseModel {CleanBefore = true};

        var cleanBefore = config!.CleanBefore;

        if (cleanBefore)
        {
            Console.WriteLine(@"Start force clean data");

            await _elasticClient.DeleteByQueryAsync<TDtoModel>(w =>
                w.Query(x => x.QueryString(q
                    => q.Query("*"))).Index(_indexName));

            await _elasticClient.Indices.DeleteAsync(_indexName);

            Console.WriteLine(@"Force clean has been comlpete!");
        }
    }

    /// <summary>
    ///     Get elastic index
    /// </summary>
    private async Task<ExistsResponse> GetIndexAsync()
    {
        return  await _elasticClient.Indices.ExistsAsync(_indexName);
    }
    
    /// <summary>
    ///     Checking index
    /// </summary>
    private async Task CheckIndexAsync(ExistsResponse index)
    {
        if (!index.Exists)
        {
            Console.WriteLine($@"Creating index {_indexName}");

            var response = await _elasticClient.Indices.CreateAsync(_indexName, r  => r.Map<TDtoModel>(x => x.AutoMap()));

            if (!response.ShardsAcknowledged) throw response.OriginalException;

            Console.WriteLine(@"Index successfully created!");
        }
    }
    
    /// <summary>
    ///     Insert Data to Elk
    /// </summary>
    /// <param name="config">Configuration model</param>
    protected virtual async Task InsertAsync(BaseModel? config)
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
            
            if (_identifierName == null)  throw new ArgumentNullException(_identifierName,@"Identifier Name can not be null");
    
            await foreach (var dto in data)
            {
                var identifierValue =  dto.GetType().GetProperty(_identifierName)?.GetValue(dto, null);

                if (identifierValue != null)
                    await _elasticClient.CreateAsync(dto, s => s.Index(_indexName).Id(identifierValue.ToString()));
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
    ///     Generate data
    /// </summary>
    /// <param name="configurationModel">Configuration model</param>
    /// <returns>List of dto model</returns>
    protected virtual async IAsyncEnumerable<TDtoModel> GenerateDataAsync(ConfigurationModel configurationModel)
    {
        for (var i = 0; i < configurationModel.Count; i++)
            yield return await CreateNewDtoAsync(configurationModel);
    }
}