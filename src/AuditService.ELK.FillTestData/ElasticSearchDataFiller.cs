using AuditService.Data.Domain.Domain;
using AuditService.Data.Domain.Dto;
using AuditService.Data.Domain.Enums;
using AuditService.WebApiApp.AppSettings;
using AuditService.WebApiApp.Services;
using AuditService.WebApiApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Nest;
using Newtonsoft.Json;
using ActionType = AuditService.Data.Domain.Enums.ActionType;

namespace AuditService.ELK.FillTestData;

/// <summary>
///     Generator test data for ELK
/// </summary>
internal class ElasticSearchDataFiller
{
    private readonly IElasticClient _elasticClient;
    private readonly IConfiguration _configuration;
    private readonly CategoryDictionary _categoryDictionary;
    private readonly Random _random;
    private readonly IReferenceService _referenceService;
    private IJsonData _jsonData;

    public ElasticSearchDataFiller(IElasticClient elasticClient, IConfiguration configuration)
    {
        _elasticClient = elasticClient;
        _configuration = configuration;
        _random = new Random();
        _categoryDictionary = new CategoryDictionary(configuration);
    }

    /// <summary>
    ///     Start generation
    /// </summary>
    public async Task ExecuteAsync()
    {
        try
        {
            var cleanBefore = _configuration.GetValue<bool>("CleanBefore");
            if (cleanBefore)
            {
                Console.WriteLine("Start force clean data");

                await _elasticClient.DeleteByQueryAsync<AuditLogTransactionDomainModel>(w => w.Query(x => x.QueryString(q => q.Query("*"))));
                await _elasticClient.Indices.DeleteAsync(_configuration["ElasticSearch:DefaultIndex"]);

                Console.WriteLine("Force clean has been comlpete!");
            }

            var index = await _elasticClient.Indices.ExistsAsync(_configuration["ElasticSearch:DefaultIndex"]);
            if (!index.Exists)
            {
                Console.WriteLine("Creating index " + _configuration["ElasticSearch:DefaultIndex"]);

                var response = await _elasticClient.Indices.CreateAsync(_configuration["ElasticSearch:DefaultIndex"], r => r.Map<AuditLogTransactionDomainModel>(x => x.AutoMap()));
                if (!response.ShardsAcknowledged)
                    throw response.OriginalException;

                Console.WriteLine("Index successfully created!");
            }

            Console.WriteLine("Get configuration for generation data");
            
            var configurationModels = _configuration.GetSection("Fillers").Get<ConfigurationModel[]>();
            foreach (var configurationModel in configurationModels)
            {
                Console.WriteLine("");
                Console.WriteLine("Configuration model:");
                Console.WriteLine(JsonConvert.SerializeObject(configurationModel, Formatting.Indented));
                
                var data = GenerateData(configurationModel);

                Console.WriteLine("Generation is completed");

                foreach (var dto in data)
                    await _elasticClient.CreateAsync(dto, s => s.Index(_configuration["ElasticSearch:DefaultIndex"]).Id(dto.EntityId));

                Console.WriteLine("Data has been saving");
                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine("All configuration models has been saving");

            Console.WriteLine($"Total records: {configurationModels.Sum(w => w.Count)}.");
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
    private IEnumerable<AuditLogTransactionDomainModel> GenerateData(ConfigurationModel configurationModel)
    {
        for (var i = 0; i < configurationModel.Count; i++)
            yield return CreateNewDto(configurationModel);
    }

    /// <summary>
    ///     Create model Dto on base configuration model
    /// </summary>
    /// <param name="configurationModel">Configuration model</param>
    private AuditLogTransactionDomainModel CreateNewDto(ConfigurationModel configurationModel)
    {
        var uid = Guid.NewGuid();
        var dto = new AuditLogTransactionDomainModel
        {
            NodeId = uid,
            Service = configurationModel.ServiceName ?? Enum.GetValues<ServiceId>().GetRandomItem(_random),
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
            ? _categoryDictionary.GetCategory(dto.Service, _random)
            : configurationModel.CategoryCode;

        return dto;
    }
}