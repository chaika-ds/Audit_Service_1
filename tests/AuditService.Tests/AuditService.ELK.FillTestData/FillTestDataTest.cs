using AuditService.ELK.FillTestData;
using AuditService.ELK.FillTestData.Generators;
using AuditService.Handlers;
using AuditService.Setup.AppSettings;
using AuditService.Tests.Fakes;
using AuditService.Tests.Resources;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Redis;

namespace AuditService.Tests.AuditService.ELK.FillTestData;

/// <summary>
///     Test of of AuditService.ELK.FillTestData
/// </summary>
public class FillTestDataTest
{
    /// <summary>
    ///     Test of dependecies injection
    /// </summary>
    [Fact]
    internal void Test_of_dependecies_injection()
    {
        _ = GetServiceProvider(TestResources.DefaultIndex);

        Assert.True(true);
    }

    /// <summary>
    ///     Test of audit log data generator
    /// </summary>
    [Fact]
    internal async Task Test_of_audit_log_data_generator()
    {
        var serviceProvider = GetServiceProvider(TestResources.DefaultIndex);

        await serviceProvider.GetRequiredService<AuditLogDataGenerator>().GenerateAsync();
        
        Assert.True(true);
    }

    /// <summary>
    ///     Test of blocked players log data generator
    /// </summary>
    [Fact]
    internal async Task Test_of_blocked_players_log_data_generator()
    {
        var serviceProvider = GetServiceProvider(TestResources.BlockedPlayersLog);

        await serviceProvider.GetRequiredService<BlockedPlayersLogDataGenerator>().GenerateAsync();

        Assert.True(true);
    }

    /// <summary>
    ///     Test of players change log data generator
    /// </summary>
    [Fact]
    internal async Task Test_of_players_change_log_data_generator()
    {
        var serviceProvider = GetServiceProvider(TestResources.PlayerChangesLog);

        await serviceProvider.GetRequiredService<PlayerChangesLogDataLogDataGenerator>().GenerateAsync();

        Assert.True(true);
    }

    /// <summary>
    ///     Test of visit log data generator
    /// </summary>
    [Fact]
    internal async Task Test_of_visit_log_data_generator()
    {
        var serviceProvider = GetServiceProvider(TestResources.VisitLog);

        await serviceProvider.GetRequiredService<VisitLogGenerator>().GenerateAsync();

        Assert.True(true);
    }

    /// <summary>
    ///     Get in meamory service provider
    /// </summary>
    private IServiceProvider GetServiceProvider(string elasticIndex)
    {
        var services = new ServiceCollection();

        services.AddLogging();
        services.AddScoped<CategoryDictionary>();
        services.AddScoped<VisitLogGenerator>();
        services.AddScoped<AuditLogDataGenerator>();
        services.AddScoped<BlockedPlayersLogDataGenerator>();
        services.AddScoped<PlayerChangesLogDataLogDataGenerator>();
        services.AddScoped<IElasticIndexSettings, FakeElasticSearchSettings>();
        DiConfigure.RegisterServices(services);
        services.AddSingleton<IRedisRepository, FakeRedisReposetoryForCachePipelineBehavior>();
        services.AddScoped(serviceProvider =>
        {
            return FakeElasticSearchClientProvider.GetFakeElasticSearchClient(elasticIndex);
        });
        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}
