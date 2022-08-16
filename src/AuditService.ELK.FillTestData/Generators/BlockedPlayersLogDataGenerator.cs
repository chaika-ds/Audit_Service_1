using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto;
using AuditService.ELK.FillTestData.Models;
using AuditService.ELK.FillTestData.Patterns.Template;
using AuditService.ELK.FillTestData.Resources;
using AuditService.Setup.AppSettings;

namespace AuditService.ELK.FillTestData.Generators;

/// <summary>
///   Blocked Players Log Generator models
/// </summary>
internal class BlockedPlayersLogDataGenerator: LogDataGenerator<BlockedPlayersLogDomainModel, BlockedPlayersConfigModel>
{
    private readonly Random _random;

    /// <summary>
    ///   Blocked Players Log Generator
    /// </summary>
    public BlockedPlayersLogDataGenerator(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        _random = new Random();
    }

    /// <summary>
    ///    Set Index of elastic
    /// </summary>
    protected override string? GetIndex(IElasticIndexSettings indexes) => $"{indexes.BlockedPlayersLog}-2022.06";
    
    /// <summary>
    ///    Set identifier of index
    /// </summary>
    protected override string GetIdentifierName() => nameof(BlockedPlayersLogResponseDto.PlayerId);
    
    /// <summary>
    ///    Override resource data
    /// </summary>
    protected override byte[]? GetResourceData() => ElkJsonResource.blockedPlayers;
    
    /// <summary>
    ///     Create model for inserting data to elastic
    /// </summary>
    /// <returns>BlockedPlayersLogResponseDto</returns>
    protected override Task<BlockedPlayersLogDomainModel> CreateNewDtoAsync()
    {
        var dto = new BlockedPlayersLogDomainModel
        {
            PlayerId = Guid.NewGuid(),
            NodeId = Guid.Parse("84b7447a-9b4e-4826-a075-6d52080d67cb"),
            PlayerLogin = "player@gmail.com",
            Language = "en",
            Timestamp = DateTime.Now.GetRandomItem(_random),
            PreviousBlockingDate = DateTime.Now.GetRandomItem(_random),
            BlockingDate = DateTime.Now.AddMonths(-2),
            Browser = "chrome",
            BlocksCounter = 3,
            BrowserVersion = "1",
            Platform = "windows",
            LastVisitIpAddress = "0.0.0.0"
        };

        return Task.FromResult(dto);
    }
}