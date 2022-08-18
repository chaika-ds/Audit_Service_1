using AuditService.Common.Enums;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.ELK.FillTestData.Models;
using AuditService.ELK.FillTestData.Patterns.Template;
using AuditService.ELK.FillTestData.Resources;
using AuditService.Setup.AppSettings;

namespace AuditService.ELK.FillTestData.Generators;

/// <summary>
///   Player Changes Log Generator models
/// </summary>
internal class PlayerChangesLogDataLogDataGenerator : LogDataGenerator<PlayerChangesLogDomainModel, PlayerChangesLogConfigModel>
{
    private readonly Random _random;

    /// <summary>
    ///   Player Changes Log Generator
    /// </summary>
    public PlayerChangesLogDataLogDataGenerator(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        _random = new Random();
    }

    /// <summary>
    ///    Set Index of elastic
    /// </summary>
    protected override string? GetIndex(IElasticIndexSettings indexes) => $"{indexes.PlayerChangesLog}-2022.06";
    
    /// <summary>
    ///    Set identifier of index
    /// </summary>
    protected override string GetIdentifierName() => nameof(PlayerChangesLogDomainModel.PlayerId);

    /// <summary>
    ///    Override resource data
    /// </summary>
    protected override byte[]? GetResourceData() => ElkJsonResource.playerChanges;
    
    /// <summary>
    ///     Create model for inserting data to elastic
    /// </summary>
    /// <returns>PlayerChangesLogResponseDto</returns>
    protected override Task<PlayerChangesLogDomainModel> CreateNewDtoAsync()
    {
        var dto = new PlayerChangesLogDomainModel
        {
            PlayerId = Guid.NewGuid(),
            NodeId = Guid.Parse("84b7447a-9b4e-4826-a075-6d52080d67cb"),
            Timestamp = DateTime.Now.AddMonths(-2),
            IpAddress = "0.0.0.0",
            Reason = "Updated",
           
            User = new UserInitiatorDomainModel
            {
                Id = Guid.NewGuid(),
                UserAgent = $"agent_{Guid.NewGuid()}",
                Email = "test@gmail.com"
            },
            EventCode = "SSO_PASSWORD_CHANGED",
            ModuleName = ModuleName.SSO.ToString(),
            EventInitiator = (ConfigurationModel?.EventInitiator ?? Enum.GetValues<EventInitiator>().GetRandomItem(_random)).ToString()
        };

        return Task.FromResult(dto);
    }
}