using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.AuditService.WebApi.Verifiers;

/// <summary>
/// Verifier for testing DI injection
/// </summary>
public sealed class ServiceCollectionVerifier
{
    private readonly ServiceCollection _serviceCollection;

    public ServiceCollectionVerifier(ServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    /// <summary>
    /// Check if singleton service was registered
    /// </summary>
    /// <typeparam name="TService">ServiceType</typeparam>
    /// <typeparam name="TInstance">ImplementationType</typeparam>
    public void ContainsSingletonService<TService, TInstance>()
    {
        IsRegistered<TService, TInstance>(ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Check if transient service was registered
    /// </summary>
    /// <typeparam name="TService">ServiceType</typeparam>
    /// <typeparam name="TInstance">ImplementationType</typeparam>
    public void ContainsTransientService<TService, TInstance>()
    {
        IsRegistered<TService, TInstance>(ServiceLifetime.Transient);
    }

    /// <summary>
    /// Check if scoped service was registered
    /// </summary>
    /// <typeparam name="TService">ServiceType</typeparam>
    /// <typeparam name="TInstance">ImplementationType</typeparam>
    public void ContainsScopedService<TService, TInstance>()
    {
        IsRegistered<TService, TInstance>(ServiceLifetime.Scoped);
    }

    /// <summary>
    /// Assert if checked service was registered
    /// </summary>
    /// <typeparam name="TService">ServiceType</typeparam>
    /// <typeparam name="TInstance">ImplementationType</typeparam>
    /// <param name="lifetime">ServiceLifetime</param>
    private void IsRegistered<TService, TInstance>(ServiceLifetime lifetime)
    {
        var serviceDescriptor = _serviceCollection.FirstOrDefault(x => x.ServiceType == typeof(TService));
        
        if (serviceDescriptor!.ServiceType.FullName!.Contains("Settings"))
        {
            Assert.Null(serviceDescriptor.ImplementationType);
        }
        else
        {
            Assert.True(serviceDescriptor.Is<TService, TInstance>(lifetime));
        }
    }
}