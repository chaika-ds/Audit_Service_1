using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.ELK.FillTestData.Models;
using AuditService.ELK.FillTestData.Patterns.Template;
using AuditService.Setup.AppSettings;
using Nest;

namespace AuditService.ELK.FillTestData.Generators;

internal class PlayerChangesGenerator : GeneratorTemplate<PlayerChangesLogResponseDto>
{
    private readonly IElasticIndexSettings _elasticIndexSettings;
    private readonly Random _random;

    public PlayerChangesGenerator(IElasticClient elasticClient,
        IElasticIndexSettings elasticIndexSettings)
        : base(elasticClient)
    {
        _elasticIndexSettings = elasticIndexSettings;
        _random = new Random();
    }

    protected override string? GetChanelName()
    {
        return _elasticIndexSettings.PlayerChangesLog;
    }

    protected override string? GetIdentifierName()
    {
        return nameof(PlayerChangesLogResponseDto.UserId);
    }


    /// <summary>
    ///     Create model Dto on base configuration model
    /// </summary>
    protected override Task<PlayerChangesLogResponseDto> CreateNewDtoAsync(ConfigurationModel configurationModel)
    {
        var uid = Guid.NewGuid();
        var dto = new PlayerChangesLogResponseDto
        {
            UserId = uid,
            Timestamp = DateTime.Now.GetRandomItem(_random),
            UserLogin = "test@gmail.com",
            IpAddress = "",
            EventKey = "",
            EventName = "",
            Reason = "",
            OldValue = new List<LocalizedPlayerAttributeDomainModel>(),
            NewValue = new List<LocalizedPlayerAttributeDomainModel>(),
        };

        return Task.FromResult(dto);
    }
}