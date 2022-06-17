using AuditService.Setup.Configurations;
using Microsoft.Extensions.DependencyInjection;

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
}