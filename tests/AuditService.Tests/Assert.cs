using AuditService.Tests.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Xunit;

/// <summary>
/// Assert for testing DI injection
/// </summary>
public partial class Assert
{
    /// <summary>
    /// Assert if checked service was registered
    /// </summary>
    /// <typeparam name="TService">ServiceType</typeparam>
    /// <typeparam name="TInstance">ImplementationType</typeparam>
    /// <param name="serviceCollection">IServiceCollection</param>
    /// <param name="lifetime">ServiceLifetime</param>
    public static void IsRegisteredService<TService, TInstance>(IServiceCollection serviceCollection,
        ServiceLifetime lifetime)
    {
        var serviceDescriptor = serviceCollection.FirstOrDefault(x => x.ServiceType == typeof(TService));
        True(serviceDescriptor?.Is<TService, TInstance>(lifetime));
    }

    /// <summary>
    /// Assert if internal service was registered
    /// </summary>
    /// <typeparam name="TService">ServiceType</typeparam>
    /// <param name="serviceCollection">IServiceCollection</param>
    /// <param name="lifetime">ServiceLifetime</param>
    public static void IsRegisteredInternalService<TService>(IServiceCollection serviceCollection,
        ServiceLifetime lifetime)
    {
        var serviceDescriptor = serviceCollection.FirstOrDefault(x => x.ServiceType == typeof(TService));
        True(serviceDescriptor?.Is<TService>(lifetime));
    }

    /// <summary>
    /// Assert if settings was registered
    /// </summary>
    /// <typeparam name="TService">ServiceType</typeparam>
    /// <param name="serviceCollection">IServiceCollection</param>
    /// <param name="lifetime">ServiceLifetime</param>
    public static void IsRegisteredSettings<TService>(IServiceCollection serviceCollection,
        ServiceLifetime lifetime)
    {
        var serviceDescriptor = serviceCollection.FirstOrDefault(x => x.ServiceType == typeof(TService));
        True(serviceDescriptor?.Is<TService>(lifetime));
    }

    /// <summary>
    /// Assert if response is received in TResponse format
    /// </summary>
    /// <typeparam name="TResponse">Response type</typeparam>
    /// <param name="response">TResponse</param>
    public static void IsPlayerChangesLogReceived<TResponse>(TResponse response)
    {
        NotNull(response!);
        IsType<TResponse>(response!);
    }
}