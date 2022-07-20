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
internal class PlayerChangesLogDataLogDataGenerator : LogDataGenerator<PlayerChangesLogResponseDto, PlayerChangesLogConfigModel>
{
    private readonly Random _random;

    /// <summary>
    ///   Player Changes Log Generator
    /// </summary>
    public PlayerChangesLogDataLogDataGenerator(IElasticClient elasticClient,
        IElasticIndexSettings elasticIndexSettings)
        : base(elasticClient, ElkJsonResource.playerChanges, elasticIndexSettings.PlayerChangesLog,nameof(PlayerChangesLogResponseDto.UserId))
    {
        _random = new Random();
    }


    /// <summary>
    ///     Create model for inserting data to elastic
    /// </summary>
    /// <returns>PlayerChangesLogResponseDto</returns>
    protected override Task<PlayerChangesLogResponseDto> CreateNewDtoAsync()
    {
        var uid = Guid.NewGuid();
        var dto = new PlayerChangesLogResponseDto
        {
            UserId = uid,
            Timestamp = DateTime.Now.GetRandomItem(_random),
            UserLogin = "test@gmail.com",
            IpAddress = "0.0.0.0",
            EventKey = "PushKey",
            EventName = "PushedName",
            Reason = "Updated",
            OldValue = new List<LocalizedPlayerAttributeDomainModel>
            {
                new ()
                {
                    Label = "old value",
                    Type = "old type",
                    Value = "old value"
                }
            },
            NewValue = new List<LocalizedPlayerAttributeDomainModel>            {
                new ()
                {
                    Label = "new value",
                    Type = "new type",
                    Value = "new value"
                }
            },
        };

        return Task.FromResult(dto);
    }
}