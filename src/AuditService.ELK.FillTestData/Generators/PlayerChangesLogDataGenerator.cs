using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.ELK.FillTestData.Models;
using AuditService.ELK.FillTestData.Patterns.Template;
using AuditService.ELK.FillTestData.Resources;
using AuditService.Setup.AppSettings;
using Nest;

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
    protected override string? GetIndex(IElasticIndexSettings indexes) => indexes.PlayerChangesLog;
    
    /// <summary>
    ///    Set identifier of index
    /// </summary>
    protected override string GetIdentifierName() => nameof(PlayerChangesLogResponseDto.UserId);

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
            ProjectId = Guid.NewGuid(),
            NodeId = Guid.NewGuid(),
            Timestamp = DateTime.Now.GetRandomItem(_random),
            IpAddress = "0.0.0.0",
            Reason = "Updated",
            OldValues = new Dictionary<string, PlayerAttributeDomainModel>(),
            NewValues = new Dictionary<string, PlayerAttributeDomainModel>(),
            User = new UserInitiatorDomainModel
            {
                Id = Guid.NewGuid(),
                UserAgent = $"agent_{Guid.NewGuid()}",
                Email = "test@gmail.com"
            },
            EventCode = "Code",
            ModuleName = ConfigurationModel?.ModuleName ?? Enum.GetValues<ModuleName>().GetRandomItem(_random),
            EventInitiator = ConfigurationModel?.EventInitiator ?? Enum.GetValues<EventInitiator>().GetRandomItem(_random),
        };

        return Task.FromResult(dto);
    }
}