using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.VisitLog;
using AuditService.ELK.FillTestData.Models;
using AuditService.ELK.FillTestData.Patterns.Template;
using AuditService.ELK.FillTestData.Resources;
using AuditService.Setup.AppSettings;

namespace AuditService.ELK.FillTestData.Generators;

/// <summary>
///     Data generator for visit log in ELK
/// </summary>
internal class VisitLogGenerator : LogDataGenerator<VisitLogDomainModel, VisitLogConfigModel>
{
    private readonly Random _random;

    public VisitLogGenerator(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _random = new Random();
    }

    /// <summary>
    ///     Create a model for the ELK
    /// </summary>
    /// <returns>Model for the ELK</returns>
    protected override Task<VisitLogDomainModel> CreateNewDtoAsync()
    {
        var randomValue = _random.Next(ConfigurationModel?.Count ?? 300);

        if (ConfigurationModel?.Type == VisitLogType.Player)
            return Task.FromResult(new VisitLogDomainModel
            {
                Type = VisitLogType.Player,
                Timestamp = DateTime.Now.AddHours(-randomValue),
                Authorization = CreateAuthorizationDataDomainModel(randomValue),
                Ip = $"127.0.0.{randomValue}",
                Login = $"login_{randomValue}",
                NodeId = Guid.NewGuid(),
                PlayerId = Guid.NewGuid()
            });

        return Task.FromResult(new VisitLogDomainModel
        {
            Type = VisitLogType.User,
            Timestamp = DateTime.Now.AddHours(-randomValue),
            Authorization = CreateAuthorizationDataDomainModel(randomValue, false),
            Ip = $"27.1.0.{randomValue}",
            Login = $"loginUser_{randomValue}",
            UserId = Guid.NewGuid(),
            NodeId = Guid.NewGuid(),
            UserRoles = new List<UserRoleDomainModel>
            {
                new($"code_{randomValue}", $"role_{randomValue}"),
                new($"code_{randomValue + 1}", $"role_{randomValue + 1}")
            }
        });
    }

    /// <summary>
    ///     Get index for ELK
    /// </summary>
    /// <param name="indexes">List of index</param>
    /// <returns>Index for ELK</returns>
    protected override string? GetIndex(IElasticIndexSettings indexes) => indexes.VisitLog;

    /// <summary>
    ///     Get the field by which identification will be made
    /// </summary>
    /// <returns>Identifier name</returns>
    protected override string GetIdentifierName() => nameof(VisitLogDomainModel.Timestamp);

    /// <summary>
    ///     Get resources to generate data
    /// </summary>
    /// <returns>Resources</returns>
    protected override byte[]? GetResourceData() => ElkJsonResource.visitLogData;

    /// <summary>
    ///     Create authorization data model
    /// </summary>
    /// <param name="randomValue">Random value</param>
    /// <param name="isPlayer">The player has been authorized</param>
    /// <returns>An object containing authorization data</returns>
    private static AuthorizationDataDomainModel CreateAuthorizationDataDomainModel(int randomValue, bool isPlayer = true)
    {
        var authorizationData = new AuthorizationDataDomainModel
        {
            OperatingSystem = $"linux_{randomValue}",
            Browser = $"opera_{randomValue}",
            DeviceType = "Desktop"
        };

        if (isPlayer)
            authorizationData.AuthorizationType = $"type_{randomValue}";

        return authorizationData;
    }
}