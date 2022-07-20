using AuditService.Tests.AuditService.WebApi.Verifiers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.AuditService.WebApi.Fakes;

/// <summary>
/// Fake ServiceCollection class for DI unit test
/// </summary>
public sealed class ServiceCollectionFake
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
    /// IServiceCollection fake
    /// </summary>
    public IServiceCollection ServiceCollection => _serviceCollectionTest;

    /// <summary>
    /// Check if ServiceCollection contains singleton service
    /// </summary>
    /// <typeparam name="TService">ServiceType</typeparam>
    /// <typeparam name="TInstance">ImplementationType</typeparam>
    public void ContainsSingletonService<TService, TInstance>()
    {
        _serviceCollectionVerifier.ContainsSingletonService<TService, TInstance>();
    }

    /// <summary>
    /// Check if ServiceCollection contains transient service
    /// </summary>
    /// <typeparam name="TService">ServiceType</typeparam>
    /// <typeparam name="TInstance">ImplementationType</typeparam>
    public void ContainsTransientService<TService, TInstance>()
    {
        _serviceCollectionVerifier.ContainsTransientService<TService, TInstance>();
    }

    /// <summary>
    /// Check if ServiceCollection contains scoped service
    /// </summary>
    /// <typeparam name="TService">ServiceType</typeparam>
    /// <typeparam name="TInstance">ImplementationType</typeparam>
    public void ContainsScopedService<TService, TInstance>()
    {
        _serviceCollectionVerifier.ContainsScopedService<TService, TInstance>();
    }
}