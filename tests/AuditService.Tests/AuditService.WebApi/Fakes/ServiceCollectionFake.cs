using AuditService.Tests.AuditService.WebApi.Verifiers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.AuditService.WebApi.Fakes;

/// <summary>
/// Fake ServiceCollection class for DI unit test
/// </summary>
public static class ServiceCollectionFake
{
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