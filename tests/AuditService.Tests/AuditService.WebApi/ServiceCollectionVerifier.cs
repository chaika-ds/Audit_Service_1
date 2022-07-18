
using KIT.Redis.Settings;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tolar.Redis;

namespace AuditService.Tests.AuditService.WebApi;

public sealed class ServiceCollectionVerifier
{
    private readonly ServiceCollection _serviceCollection;

    public ServiceCollectionVerifier(ServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public void ContainsSingletonService<TService, TInstance>()
    {
        this.IsRegistered<TService, TInstance>(ServiceLifetime.Singleton);
    }

    public void ContainsTransientService<TService, TInstance>()
    {
        this.IsRegistered<TService, TInstance>(ServiceLifetime.Transient);
    }

    public void ContainsScopedService<TService, TInstance>()
    {
        this.IsRegistered<TService, TInstance>(ServiceLifetime.Scoped);
    }

    private void IsRegistered<TService, TInstance>(ServiceLifetime lifetime)
    {

        var cc = typeof(TInstance);
        var serviceDescriptor = _serviceCollection.FirstOrDefault(x => x.ServiceType == typeof(TService));
        
        if (serviceDescriptor!.ServiceType.FullName!.Contains("Settings"))
        {
            Assert.Null(serviceDescriptor.ImplementationType);
        }
        else
        {
            Assert.Equal(typeof(TInstance), serviceDescriptor.ImplementationType);
            Assert.Equal(serviceDescriptor?.Lifetime, lifetime);
        }
    }
}