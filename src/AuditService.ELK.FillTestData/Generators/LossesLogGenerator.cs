using AuditService.Common.Models.Domain.LossesLog;
using AuditService.ELK.FillTestData.Models;
using AuditService.ELK.FillTestData.Patterns.Template;
using AuditService.ELK.FillTestData.Resources;
using AuditService.Setup.AppSettings;

namespace AuditService.ELK.FillTestData.Generators;

/// <summary>
///     Losses log generator
/// </summary>
internal class LossesLogGenerator : LogDataGenerator<LossesLogDomainModel, BlockedPlayersConfigModel>
{
    private readonly Random _random;

    public LossesLogGenerator(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _random = new Random();
    }

    /// <summary>
    ///     Create new model
    /// </summary>
    /// <returns>Domain model</returns>
    protected override Task<LossesLogDomainModel> CreateNewDtoAsync()
    {
        return Task.FromResult(new LossesLogDomainModel
        {
            NodeId = Guid.Parse("84b7447a-9b4e-4826-a075-6d52080d67cb"),
            PlayerId = Guid.NewGuid(),
            Login = $"login_{_random.Next()}",
            CreateDate = DateTime.Now.AddMonths(-2),
            CurrencyCode = $"CurrencyCode_{_random.Next()}",
            LastDeposit = _random.Next()
        });
    }

    /// <summary>
    ///     Get index
    /// </summary>
    /// <param name="indexes">All indexes</param>
    /// <returns>Index</returns>
    protected override string? GetIndex(IElasticIndexSettings indexes) => $"{indexes.LossesLog}-2022.06";

    /// <summary>
    ///     Get identifier name
    /// </summary>
    /// <returns>Identifier name</returns>
    protected override string GetIdentifierName() => nameof(LossesLogDomainModel.PlayerId);

    /// <summary>
    ///     Get resource data
    /// </summary>
    /// <returns>Resource data</returns>
    protected override byte[]? GetResourceData() => ElkJsonResource.blockedPlayers;
}