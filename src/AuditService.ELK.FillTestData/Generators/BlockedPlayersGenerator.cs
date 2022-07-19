using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.ELK.FillTestData.Models;
using AuditService.ELK.FillTestData.Patterns.Template;
using AuditService.Setup.AppSettings;
using Nest;
using Newtonsoft.Json;

namespace AuditService.ELK.FillTestData.Generators;

internal class BlockedPlayersGenerator: GeneratorTemplate<BlockedPlayersLogResponseDto, BlockedPlayersLogGeneratorModel>
{
    private readonly IElasticClient _elasticClient;
    private readonly IElasticIndexSettings _elasticIndexSettings;
    private readonly Random _random;

    public BlockedPlayersGenerator(IElasticClient elasticClient,
        IElasticIndexSettings elasticIndexSettings)
        : base(elasticClient)
    {
        _elasticClient = elasticClient;
        _elasticIndexSettings = elasticIndexSettings;
        _random = new Random();
    }

    protected override string? GetChanelName()
    {
        return _elasticIndexSettings.BlockedPlayersLog;
    }

    protected override byte[] GetResourceData()
    {
        return Array.Empty<byte>();
    }

    protected override async Task InsertAsync(object config)
    {
        Console.WriteLine(@"Get configuration for generation data");

        var configurationModels = (config as BlockedPlayersLogGeneratorModel)!.Fillers;

        foreach (var configurationModel in configurationModels)
        {
            Console.WriteLine("");
            Console.WriteLine(@"Configuration model:");
            Console.WriteLine(JsonConvert.SerializeObject(configurationModel, Formatting.Indented));

            var data = GenerateDataAsync(configurationModel);

            Console.WriteLine($@"Generation {configurationModel} is completed");

            await foreach (var dto in data)
            {
                await _elasticClient.CreateAsync(dto, s => s.Index(GetChanelName()).Id(dto.PlayerId));
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
    /// <param name="playerChangesLogConfigurationModel">Configuration model</param>
    private async IAsyncEnumerable<BlockedPlayersLogResponseDto> GenerateDataAsync(BlockedPlayersLogConfigurationModel playerChangesLogConfigurationModel)
    {
        for (var i = 0; i < playerChangesLogConfigurationModel.Count; i++)
            yield return await CreateNewDtoAsync();
    }

    /// <summary>
    ///     Create model Dto on base configuration model
    /// </summary>
    private Task<BlockedPlayersLogResponseDto> CreateNewDtoAsync()
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