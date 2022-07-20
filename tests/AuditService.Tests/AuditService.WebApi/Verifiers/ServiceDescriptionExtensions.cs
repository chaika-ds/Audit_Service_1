using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.AuditService.WebApi.Verifiers;

/// <summary>
/// ServiceDescriptionExtensions class
/// </summary>
public static class ServiceDescriptionExtensions
{
    /// <summary>
    /// Check if checked service is in the list of injected services
    /// </summary>
    /// <typeparam name="TService">ServiceType</typeparam>
    /// <typeparam name="TInstance">ImplementationType</typeparam>
    /// <param name="serviceDescriptor">ServiceDescriptor</param>
    /// <param name="lifetime">ServiceLifetime</param>
    /// <returns>Is injected or not</returns>
    public static bool Is<TService, TInstance>(this ServiceDescriptor serviceDescriptor, ServiceLifetime lifetime)
    {
        var cc = serviceDescriptor.ServiceType == typeof(TService) &&
                 serviceDescriptor.ImplementationType == typeof(TInstance) &&
                 serviceDescriptor.Lifetime == lifetime;
        return cc;
    }

    public static bool Is<TService>(this ServiceDescriptor serviceDescriptor, ServiceLifetime lifetime)
    {
        var cc = serviceDescriptor.ServiceType == typeof(TService) &&
                 serviceDescriptor.Lifetime == lifetime;
        return cc;
    }
}