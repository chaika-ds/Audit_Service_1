using AuditService.Handlers;
using AuditService.Setup.AppSettings;
using AuditService.Tests.AuditService.GetAuditLog.Models;
using AuditService.Tests.Resources;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Redis;

namespace AuditService.Tests.Factories.Fakes;

/// <summary>
/// Class for fake service providers
/// </summary>
internal class FakeServiceProvider
{
    /// <summary>
    /// Create Fake ServiceProvider for testing ELK client
    /// </summary>
    /// <typeparam name="TLogType">Elastic client content type</typeparam>
    /// <param name="elasticClientJsonContent">JsonContent for Elastic client</param>
    /// <returns>IServiceProvider</returns>
    internal static IServiceProvider CreateElkServiceProviderFake<TLogType>(byte[] elasticClientJsonContent)
    {
        var services = new ServiceCollection();

        DiConfigure.RegisterServices(services);
        services.AddSingleton<IRedisRepository, FakeRedisReposetoryForCachePipelineBehavior>();
        services.AddScoped<IElasticIndexSettings, FakeElasticSearchSettings>();
        services.AddLogging();
        services.AddScoped(_ => FakeElasticSearchClientProvider.GetFakeElasticSearchClient<TLogType>(elasticClientJsonContent, TestResources.PlayerChangesLog));

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}