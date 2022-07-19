using AuditService.Common.Models.Dto;
using AuditService.ELK.FillTestData.Models;
using AuditService.ELK.FillTestData.Patterns.Template;
using AuditService.Setup.AppSettings;
using Nest;

namespace AuditService.ELK.FillTestData.Generators;

internal class BlockedPlayersGenerator: GeneratorTemplate<BlockedPlayersLogResponseDto>
{
    private readonly IElasticIndexSettings _elasticIndexSettings;
    private readonly Random _random;

    public BlockedPlayersGenerator(IElasticClient elasticClient,
        IElasticIndexSettings elasticIndexSettings)
        : base(elasticClient)
    {
        _elasticIndexSettings = elasticIndexSettings;
        _random = new Random();
    }

    protected override string? GetChanelName()
    {
        return _elasticIndexSettings.BlockedPlayersLog;
    }

    protected override string? GetIdentifierName()
    {
        return nameof(BlockedPlayersLogResponseDto.PlayerId);
    }


    /// <summary>
    ///     Create model Dto on base configuration model
    /// </summary>
    protected override Task<BlockedPlayersLogResponseDto> CreateNewDtoAsync(ConfigurationModel configurationModel)
    {
        var uid = Guid.NewGuid();
        var dto = new BlockedPlayersLogResponseDto
        {
            PlayerId = uid,
            HallId = uid,
            PlayerLogin = "player@gmail.com",
            PlayerIp = "",
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