using AuditService.ELK.FillTestData.Models;
using AuditService.Setup.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Newtonsoft.Json;

namespace AuditService.ELK.FillTestData.Patterns.Template;

/// <summary>
///    Log Data Generator model
/// </summary>
internal abstract class LogDataGenerator<TDtoModel,TConfig>
    where TDtoModel : class
    where TConfig : BaseConfig
{
    private readonly IElasticClient _elasticClient;
    private readonly IElasticIndexSettings _elasticIndexSettings;
    protected TConfig? ConfigurationModel;

    /// <summary>
    ///  Initialize LogDataGenerator
    /// </summary>
    protected LogDataGenerator(IServiceProvider serviceProvider)
    {
        _elasticClient = serviceProvider.GetRequiredService<IElasticClient>();
        _elasticIndexSettings = serviceProvider.GetRequiredService<IElasticIndexSettings>();
    }

    /// <summary>
    ///    Create new Dto model
    /// </summary>
    protected abstract Task<TDtoModel> CreateNewDtoAsync();
    
    /// <summary>
    ///    Get Index of elastic
    /// </summary>
    protected abstract string? GetIndex(IElasticIndexSettings indexes);
    
    /// <summary>
    ///    Get identifier of index
    /// </summary>
    protected abstract string GetIdentifierName();
    
    /// <summary>
    ///    Get resource data
    /// </summary>
    protected abstract byte[]? GetResourceData();
    
    /// <summary>
    ///    Execute template method
    /// </summary>
    public async Task GenerateAsync()
    {
        var config = JsonConvert.DeserializeObject<BaseModel<TConfig>>(System.Text.Encoding.Default.GetString(GetResourceData()!));
        
        await CleanBeforeAsync(config); 
        
        var index = await GetIndexAsync();
        
        await CheckAndCreateIndexAsync(index);
        
        await InsertAsync(config);
    }

    /// <summary>
    ///     Cleaning data from elastic
    /// </summary>
    /// <param name="config">Configuration model</param>
    private async Task CleanBeforeAsync(BaseModel<TConfig>? config)
    {
        var cleanBefore = config!.CleanBefore;

        if (cleanBefore)
        {
            Console.WriteLine(@"Start force clean data");

            await _elasticClient.DeleteByQueryAsync<TDtoModel>(w =>
                w.Query(x => x.QueryString(q
                    => q.Query("*"))).Index(GetIndex(_elasticIndexSettings)));

            await _elasticClient.Indices.DeleteAsync(GetIndex(_elasticIndexSettings));

            Console.WriteLine(@"Force clean has been comlpete!");
        }
    }

    /// <summary>
    ///     Get elastic index
    /// </summary>
    private async Task<ExistsResponse> GetIndexAsync()
    {
        return  await _elasticClient.Indices.ExistsAsync(GetIndex(_elasticIndexSettings));
    }
    
    /// <summary>
    ///     Checking index
    /// </summary>
    private async Task CheckAndCreateIndexAsync(ExistsResponse index)
    {
        if (!index.Exists)
        {
            Console.WriteLine($@"Creating index {GetIndex(_elasticIndexSettings)}");

            var response = await _elasticClient.Indices.CreateAsync(GetIndex(_elasticIndexSettings), r  => r.Map<TDtoModel>(x => x.AutoMap()));

            if (!response.ShardsAcknowledged) throw response.OriginalException;

            Console.WriteLine(@"Index successfully created!");
        }
    }
    
    /// <summary>
    ///     Insert Data to Elk
    /// </summary>
    /// <param name="config">Configuration model</param>
    private  async Task InsertAsync(BaseModel<TConfig>? config)
    {
        Console.WriteLine(@"Get configuration for generation data");

        var configurationModels = config!.Fillers;

        foreach (var configurationModel in configurationModels)
        {
            Console.WriteLine("");
            Console.WriteLine(@"Configuration model:");
            Console.WriteLine(JsonConvert.SerializeObject(configurationModel, Formatting.Indented));

            var data = GenerateDataAsync(configurationModel);
            
            Console.WriteLine($@"Generation {GetIdentifierName()} is completed");
            
            if (GetIdentifierName() == null)  throw new ArgumentNullException(GetIdentifierName(),@"Identifier Name can not be null");
    
            await foreach (var dto in data)
            {
                var identifierValue =  dto.GetType().GetProperty(GetIdentifierName())?.GetValue(dto, null);

                if (identifierValue != null)
                    await _elasticClient.CreateAsync(dto, s => s.Index(GetIndex(_elasticIndexSettings)).Id(identifierValue.ToString()));
            }

            Console.WriteLine(@"Data has been saved");
            Console.WriteLine("");
        }

        Console.WriteLine("");
        Console.WriteLine(@"All configuration models has been saved");

        Console.WriteLine($@"Total records: {configurationModels.Sum(w => w.Count)}.");
    }
    
    /// <summary>
    ///     Generate data
    /// </summary>
    /// <param name="configurationModel">Configuration model</param>
    /// <returns>List of dto model</returns>
    private async IAsyncEnumerable<TDtoModel> GenerateDataAsync(TConfig configurationModel)
    {
        for (var i = 0; i < configurationModel.Count; i++)
        {
            ConfigurationModel = configurationModel;
            yield return await CreateNewDtoAsync();
        }
    }
}