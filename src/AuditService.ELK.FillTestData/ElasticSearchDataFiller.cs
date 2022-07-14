using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.ELK.FillTestData.Models;
using AuditService.ELK.FillTestData.Resources;
using AuditService.Setup.AppSettings;
using Microsoft.Extensions.Configuration;
using Nest;
using Newtonsoft.Json;
using ActionType = AuditService.Common.Enums.ActionType;

namespace AuditService.ELK.FillTestData;

/// <summary>
///     Generator test data for ELK
/// </summary>
public class ElasticSearchDataFiller
{
    private readonly IElasticClient _elasticClient;
    private readonly IConfiguration _configuration;
    private readonly CategoryDictionary _categoryDictionary;
    private readonly IElasticIndexSettings _elasticIndexSettings;
    private readonly Random _random;

    public ElasticSearchDataFiller(IElasticClient elasticClient, IConfiguration configuration,
        CategoryDictionary categoryDictionary, IElasticIndexSettings elasticIndexSettings)
    {
        _elasticClient = elasticClient;
        _configuration = configuration;
        _random = new Random();
        _categoryDictionary = categoryDictionary;
        _elasticIndexSettings = elasticIndexSettings;
    }

    /// <summary>
    ///     Start generation
    /// </summary>
    public async Task ExecuteAsync()
    {
        try
        {
            var elcFillerConfig = JsonConvert.DeserializeObject<AppSettingModel>(System.Text.Encoding.Default.GetString(ElcJsonResource.elkFillData));

            var cleanBefore = elcFillerConfig!.CleanBefore;

            if (cleanBefore)
            {
                Console.WriteLine(@"Start force clean data");

                await _elasticClient.DeleteByQueryAsync<AuditLogTransactionDomainModel>(w =>
                    w.Query(x => x.QueryString(q => q.Query("*"))).Index(_elasticIndexSettings.AuditLog));
                await _elasticClient.Indices.DeleteAsync(_elasticIndexSettings.AuditLog);

                Console.WriteLine(@"Force clean has been comlpete!");
            }

            var index = await _elasticClient.Indices.ExistsAsync(_elasticIndexSettings.AuditLog);

            if (!index.Exists)
            {
                Console.WriteLine($@"Creating index {_elasticIndexSettings.AuditLog}");

                var response = await _elasticClient.Indices.CreateAsync(_elasticIndexSettings.AuditLog,
                    r => r.Map<AuditLogTransactionDomainModel>(x => x.AutoMap()));
                if (!response.ShardsAcknowledged)
                    throw response.OriginalException;

                Console.WriteLine(@"Index successfully created!");
            }

            Console.WriteLine(@"Get configuration for generation data");

            var configurationModels = elcFillerConfig.Fillers;

            foreach (var configurationModel in configurationModels)
            {
                Console.WriteLine("");
                Console.WriteLine(@"Configuration model:");
                Console.WriteLine(JsonConvert.SerializeObject(configurationModel, Formatting.Indented));

                var data = GenerateDataAsync(configurationModel);
                Console.WriteLine($@"Generation {configurationModel.ServiceName} is completed");

                await foreach (var dto in data)
                {
                    await _elasticClient.CreateAsync(dto,
                        s => s.Index(_elasticIndexSettings.AuditLog).Id(dto.EntityId));
                }

                Console.WriteLine(@"Data has been saved");
                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine(@"All configuration models has been saved");

            Console.WriteLine($@"Total records: {configurationModels.Sum(w => w.Count)}.");

            Thread.Sleep(10000);
            Environment.Exit(1);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
        }
    }

    /// <summary>
    ///     Data generation
    /// </summary>
    /// <param name="configurationModel">Configuration model</param>
    private async IAsyncEnumerable<AuditLogTransactionDomainModel> GenerateDataAsync(
        ConfigurationModel configurationModel)
    {
        for (var i = 0; i < configurationModel.Count; i++)

            yield return await CreateNewDtoAsync(configurationModel);
    }

    /// <summary>
    ///     Create model Dto on base configuration model
    /// </summary>
    /// <param name="configurationModel">Configuration model</param>
    private async Task<AuditLogTransactionDomainModel> CreateNewDtoAsync(ConfigurationModel configurationModel)
    {
        var uid = Guid.NewGuid();
        var dto = new AuditLogTransactionDomainModel
        {
            NodeId = uid,
            ModuleName = configurationModel.ServiceName ?? Enum.GetValues<ServiceStructure>().GetRandomItem(_random),
            Node = configurationModel.NodeType ?? Enum.GetValues<NodeType>().GetRandomItem(_random),
            Action = configurationModel.ActionName ?? Enum.GetValues<ActionType>().GetRandomItem(_random),
            RequestUrl = "PUT: contracts/contractId?param=value",
            RequestBody = "{ 'myjson': 0 }",
            Timestamp = DateTime.Now.GetRandomItem(_random),
            EntityName = nameof(AuditLogTransactionDomainModel),
            EntityId = Guid.NewGuid(),
            OldValue = "{ 'value': '0' }",
            NewValue = "{ 'value': '1' }",
            ProjectId = Guid.NewGuid(),
            User = new IdentityUserDomainModel
            {
                Id = uid,
                Ip = "127.0.0.0",
                Login = $"login_{uid}",
                UserAgent = $"agent_{uid}"
            }
        };

        dto.CategoryCode = string.IsNullOrEmpty(configurationModel.CategoryCode)
            ? await _categoryDictionary.GetCategoryAsync(dto.ModuleName, _random)
            : configurationModel.CategoryCode;

        return dto;
    }
}