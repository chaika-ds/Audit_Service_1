using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.AuditLog;
using AuditService.ELK.FillTestData.Models;
using AuditService.ELK.FillTestData.Patterns.Template;
using AuditService.ELK.FillTestData.Resources;
using AuditService.Setup.AppSettings;
using ActionType = AuditService.Common.Enums.ActionType;
using Nest;
using Newtonsoft.Json;

namespace AuditService.ELK.FillTestData.Generators;

/// <summary>
///   Audit Log Generator models
/// </summary>
internal class AuditLogGenerator : GeneratorTemplate<AuditLogTransactionDomainModel, AuditLogGeneratorModel>
{
    private readonly IElasticClient _elasticClient;
    private readonly IElasticIndexSettings _elasticIndexSettings;
    private readonly CategoryDictionary _categoryDictionary;
    private readonly Random _random;

    
    /// <summary>
    ///   Initialize Audit Log Generator
    /// </summary>
    public AuditLogGenerator(
        IElasticClient elasticClient,
        IElasticIndexSettings elasticIndexSettings,
        CategoryDictionary categoryDictionary) 
        : base(elasticClient)
    {
        _elasticClient = elasticClient;
        _elasticIndexSettings = elasticIndexSettings;
        _categoryDictionary = categoryDictionary;

        _random = new Random();
    }

    /// <summary>
    ///     Get channel name 
    /// </summary>
    /// <returns>Channel Name</returns>
    protected override string? GetChanelName()
    {
        return _elasticIndexSettings.AuditLog;
    }

    /// <summary>
    ///     Get resource object 
    /// </summary>
    /// <returns>Resource data as byte</returns>
    protected override byte[] GetResourceData()
    {
        return ElcJsonResource.elkFillData;
    }

    /// <summary>
    ///     Override InsertAsync with you logic
    /// </summary>
    /// <param name="config">Configuration model</param>
    protected override async Task InsertAsync(object config)
    {
            Console.WriteLine(@"Get configuration for generation data");

            var configurationModels = (config as AuditLogGeneratorModel)!.Fillers;

            foreach (var configurationModel in configurationModels)
            {
                Console.WriteLine("");
                Console.WriteLine(@"Configuration model:");
                Console.WriteLine(JsonConvert.SerializeObject(configurationModel, Formatting.Indented));

                var data = GenerateDataAsync(configurationModel);

                Console.WriteLine($@"Generation {configurationModel.ServiceName} is completed");

                await foreach (var dto in data)
                {
                    await _elasticClient.CreateAsync(dto, s => s.Index(GetChanelName()).Id(dto.EntityId));
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
    /// <param name="auditLogConfigurationModel">Configuration model</param>
    private async IAsyncEnumerable<AuditLogTransactionDomainModel> GenerateDataAsync(AuditLogConfigurationModel auditLogConfigurationModel)
    {
        for (var i = 0; i < auditLogConfigurationModel.Count; i++)
            yield return await CreateNewDtoAsync(auditLogConfigurationModel);
    }

    /// <summary>
    ///     Create model Dto on base configuration model
    /// </summary>
    /// <param name="auditLogConfigurationModel">Configuration model</param>
    /// <returns>AuditLogTransactionDomainModel</returns>
    private async Task<AuditLogTransactionDomainModel> CreateNewDtoAsync(AuditLogConfigurationModel auditLogConfigurationModel)
    {
        var uid = Guid.NewGuid();
        var dto = new AuditLogTransactionDomainModel
        {
            NodeId = uid,
            ModuleName = auditLogConfigurationModel.ServiceName ?? Enum.GetValues<ModuleName>().GetRandomItem(_random),
            Node = auditLogConfigurationModel.NodeType ?? Enum.GetValues<NodeType>().GetRandomItem(_random),
            Action = auditLogConfigurationModel.ActionName ?? Enum.GetValues<ActionType>().GetRandomItem(_random),
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

        dto.CategoryCode = string.IsNullOrEmpty(auditLogConfigurationModel.CategoryCode)
            ? await _categoryDictionary.GetCategoryAsync(dto.ModuleName, _random)
            : auditLogConfigurationModel.CategoryCode;

        return dto;
    }
    
}