using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.AuditLog;
using AuditService.ELK.FillTestData.Models;
using AuditService.ELK.FillTestData.Patterns.Template;
using AuditService.ELK.FillTestData.Resources;
using AuditService.Setup.AppSettings;
using ActionType = AuditService.Common.Enums.ActionType;

namespace AuditService.ELK.FillTestData.Generators;

/// <summary>
///   Audit Log Generator models
/// </summary>
internal class AuditLogDataGenerator : LogDataGenerator<AuditLogTransactionDomainModel, AuditLogConfigModel>
{
    private readonly CategoryDictionary _categoryDictionary;
    private readonly Random _random;

    
    /// <summary>
    ///   Initialize Audit Log Generator
    /// </summary>
    public AuditLogDataGenerator(
        IServiceProvider serviceProvider,
        CategoryDictionary categoryDictionary) 
        : base(serviceProvider)
    {
        _categoryDictionary = categoryDictionary;

        _random = new Random();
    }

    /// <summary>
    ///    Set Index of elastic
    /// </summary>
    protected override string? GetIndex(IElasticIndexSettings indexes) => indexes.AuditLog;
    
    /// <summary>
    ///    Set identifier of index
    /// </summary>
    protected override string GetIdentifierName() => nameof(AuditLogTransactionDomainModel.EntityId);
    
    /// <summary>
    ///    Override resource data
    /// </summary>
    protected override byte[]? GetResourceData() => ElkJsonResource.auditLog;


    /// <summary>
    ///     Create model for inserting data to elastic
    /// </summary>
    /// <returns>AuditLogTransactionDomainModel</returns>
    protected override async Task<AuditLogTransactionDomainModel> CreateNewDtoAsync()
    {
        var uid = Guid.NewGuid();
        var dto = new AuditLogTransactionDomainModel
        {
            EntityId = Guid.NewGuid().ToString(),
            ProjectId = Guid.NewGuid(),
            NodeId = Guid.NewGuid(),
            ModuleName = ConfigurationModel?.ServiceName ?? Enum.GetValues<ModuleName>().GetRandomItem(_random),
            NodeType = ConfigurationModel?.NodeType ?? Enum.GetValues<NodeType>().GetRandomItem(_random),
            ActionName = ConfigurationModel?.ActionName ?? Enum.GetValues<ActionType>().GetRandomItem(_random),
            RequestUrl = "PUT: contracts/contractId?param=value",
            RequestBody = "{ 'myjson': 0 }",
            Timestamp = DateTime.Now.GetRandomItem(_random),
            EntityName = nameof(AuditLogTransactionDomainModel),
            OldValue = "{ 'value': '0' }",
            NewValue = "{ 'value': '1' }",
            User = new IdentityUserDomainModel
            {
                Id = uid,
                Ip = "127.0.0.0",
                Email = $"login_{uid}",
                UserAgent = $"agent_{uid}"
            }
        };

        dto.CategoryCode = string.IsNullOrEmpty(ConfigurationModel?.CategoryCode)
            ? await _categoryDictionary.GetCategoryAsync(dto.ModuleName, _random)
            : ConfigurationModel.CategoryCode;

        return dto;
    }
}