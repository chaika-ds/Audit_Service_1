using AuditService.Setup;
using AuditService.Setup.ServiceConfigurations;
using bgTeam.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AuditService.Tests.AuditService.Setup;

public class ElasticSearchConfigurationTest
{
    private readonly IServiceCollection _services;

    public ElasticSearchConfigurationTest()
    {
        _services = new ServiceCollection();
    }

    [Fact]
    public void AddElasticSearchTest()
    {
        _services.AddElasticSearch();

        Assert.False(false, "true");
    }

    [Fact]
    public async Task RedisCacheConfiguration_AddRedisCache_TestAsync()
    {
        var services = new ServiceCollection();
        
        services.RegisterSettings();
        services.AddRedisCache();

        var serviceProvider = services.BuildServiceProvider();
        var cache = serviceProvider.GetRequiredService<IDistributedCache>();

        var bytesArray = new byte[] { 0, 12, 44, 55, 90 };

        await cache.SetAsync("test_cache_key", bytesArray);

        Thread.Sleep(100);

        var result = await cache.GetAsync("test_cache_key");

        Assert.NotNull(result);
        Assert.NotEqual(result, bytesArray);
    }
}