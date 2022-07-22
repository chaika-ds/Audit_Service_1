using AuditService.Tests.AuditService.WebApi.Verifiers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.AuditService.WebApi.Fakes;

/// <summary>
/// Fake ServiceCollection class for DI unit test
/// </summary>
public static class ServiceCollectionFake
{
    private readonly ServiceCollection _serviceCollectionTest;
    private readonly ServiceCollectionVerifier _serviceCollectionVerifier;

    public ServiceCollectionFake()
    {
        _serviceCollectionTest = new ServiceCollection();
        _serviceCollectionTest.AddScoped<IConfiguration, ConfigurationFake>();

        _serviceCollectionVerifier = new ServiceCollectionVerifier(_serviceCollectionTest);
    }

    /// <summary>
    /// Fake IServiceCollection for unit test
    /// </summary>
    public static IServiceCollection AddTestServiceCollection()
    {
        var serviceCollectionFake = new ServiceCollection();
        serviceCollectionFake.AddScoped<IConfiguration, ConfigurationFake>();

        return serviceCollectionFake;
    }
}