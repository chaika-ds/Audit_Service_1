using AuditService.Common.Models.Dto;
using AuditService.ELK.FillTestData.Models;
using AuditService.ELK.FillTestData.Patterns.Template;
using AuditService.Setup.AppSettings;
using Nest;

namespace AuditService.ELK.FillTestData.Generators;

/// <summary>
///   Blocked Players Log Generator models
/// </summary>
internal class BlockedPlayersLogDataGenerator: LogDataGenerator<BlockedPlayersLogResponseDto>
{
    private readonly Random _random;

    /// <summary>
    ///   Blocked Players Log Generator
    /// </summary>
    public BlockedPlayersLogDataGenerator(IElasticClient elasticClient,
        IElasticIndexSettings elasticIndexSettings)
        : base(elasticClient,elasticIndexSettings.BlockedPlayersLog,nameof(BlockedPlayersLogResponseDto.PlayerId))
    {
        _random = new Random();
    }


    /// <summary>
    ///     Create model for inserting data to elastic
    /// </summary>
    /// <param name="configurationModel">Configuration model</param>
    /// <returns>BlockedPlayersLogResponseDto</returns>
    protected override Task<BlockedPlayersLogResponseDto> CreateNewDtoAsync(ConfigurationModel? configurationModel)
    {
        var uid = Guid.NewGuid();
        var dto = new BlockedPlayersLogResponseDto
        {
            PlayerId = uid,
            HallId = uid,
            PlayerLogin = "player@gmail.com",
            PlayerIp = "0.0.0.0",
            Language = "en",
            Timestamp = DateTime.Now.GetRandomItem(_random),
            PreviousBlockingDate = DateTime.Now.GetRandomItem(_random),
            BlockingDate = DateTime.Now.GetRandomItem(_random),
            OperatingSystem = "windows",
            Browser = "chrome",
            BlocksCounter = 3,
            BrowserVersion = "1"
        };

        return Task.FromResult(dto);
    }
}