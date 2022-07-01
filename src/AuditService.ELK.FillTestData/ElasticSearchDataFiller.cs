using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using Microsoft.Extensions.Configuration;
using Nest;
using Newtonsoft.Json;
using ActionType = AuditService.Common.Enums.ActionType;

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

    private const string ElkIndexAuditLog = "ELASTIC_SEARCH:INDEXES:ELK_INDEX_AUDITLOG";

    public ElasticSearchDataFiller(IElasticClient elasticClient, IConfiguration configuration)
    {
        _elasticClient = elasticClient;
        _configuration = configuration;
        _random = new Random();
        _categoryDictionary = new CategoryDictionary();
    }

    /// <summary>
    ///     Start generation
    /// </summary>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            var cleanBefore = _configuration.GetValue<bool>("CleanBefore");

            if (cleanBefore)
            {
                Console.WriteLine(@"Start force clean data");

                await _elasticClient.DeleteByQueryAsync<AuditLogTransactionDomainModel>(w => w.Query(x => x.QueryString(q => q.Query("*"))).Index(_configuration[ElkIndexAuditLog]), cancellationToken);
                await _elasticClient.Indices.DeleteAsync(_configuration[ElkIndexAuditLog], null, cancellationToken);

                Console.WriteLine(@"Force clean has been completed!");
            }

            var index = await _elasticClient.Indices.ExistsAsync(_configuration[ElkIndexAuditLog], null, cancellationToken);

            if (!index.Exists)
            {
                Console.WriteLine($@"Creating index {_configuration[ElkIndexAuditLog]}");

                var response = await _elasticClient.Indices.CreateAsync(_configuration[ElkIndexAuditLog], r => r.Map<AuditLogTransactionDomainModel>(x => x.AutoMap()), cancellationToken);
                if (!response.ShardsAcknowledged)
                    throw response.OriginalException;

                Console.WriteLine(@"Index successfully created!");
            }

            Console.WriteLine(@"Get configuration for generation data");

            var configurationModels = _configuration.GetSection("Fillers").Get<ConfigurationModel[]>();

            foreach (var configurationModel in configurationModels)
            {
                Console.WriteLine(@"");
                Console.WriteLine(@"Configuration model:");
                Console.WriteLine(JsonConvert.SerializeObject(configurationModel, Formatting.Indented));

                var data =  GenerateDataAsync(configurationModel, cancellationToken);
                Console.WriteLine($@"Generation {configurationModel.ServiceName} is completed");

                await foreach (var dto in data.WithCancellation(cancellationToken))
                {
                    await _elasticClient.CreateAsync(dto, s => s.Index(_configuration[ElkIndexAuditLog]).Id(dto.EntityId), cancellationToken);
                }                        

                Console.WriteLine(@"Data has been saving");
                Console.WriteLine(@"");
            }

            Console.WriteLine(@"");
            Console.WriteLine(@"All configuration models has been saving");

            Console.WriteLine($@"Total records: {configurationModels.Sum(w => w.Count)}.");
        }
        catch (Exception e)
        {
            Console.WriteLine(@"Error: " + e);
        }
    }

    /// <summary>
    ///     Data generation
    /// </summary>
    /// <param name="configurationModel">Configuration model</param>
    private async IAsyncEnumerable<AuditLogTransactionDomainModel> GenerateDataAsync(ConfigurationModel configurationModel, CancellationToken cancellationToken)
    {
        for (var i = 0; i < configurationModel.Count; i++)

            yield return await CreateNewDtoAsync(configurationModel, cancellationToken);
    }

    /// <summary>
    ///     Create model Dto on base configuration model
    /// </summary>
    /// <param name="configurationModel">Configuration model</param>
    private async Task<AuditLogTransactionDomainModel> CreateNewDtoAsync(ConfigurationModel configurationModel, CancellationToken cancellationToken)
    {
        var uid = Guid.NewGuid();
        var dto = new AuditLogTransactionDomainModel
        {
            NodeId = uid,
            Service = configurationModel.ServiceName ?? Enum.GetValues<ServiceStructure>().GetRandomItem(_random),
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
            ? await _categoryDictionary.GetCategoryAsync(dto.Service, _random, cancellationToken)
            : configurationModel.CategoryCode;

        return dto;
    }
}