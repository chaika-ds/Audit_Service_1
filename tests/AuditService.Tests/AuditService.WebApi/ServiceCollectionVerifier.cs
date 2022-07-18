using bgTeam.Extensions;
using KIT.Redis.Settings;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tolar.Redis;

namespace AuditService.Tests.AuditService.WebApi;

public sealed class ServiceCollectionVerifier
{
    private readonly Mock<IServiceCollection> _serviceCollectionMock;

    public ServiceCollectionVerifier()
    {
        _serviceCollectionMock = new Mock<IServiceCollection>();
        //_serviceCollectionMock.Setup(x =>
        //    x.AddSettings<IRedisSettings, RedisSettings>()).Returns(_serviceCollectionMock.Object);
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
        _serviceCollectionMock
            .Verify(serviceCollection => serviceCollection.Contains(It.Is<ServiceDescriptor>(serviceDescriptor => serviceDescriptor.Is<TService, TInstance>(lifetime))));
            

        //_serviceCollectionMock
        //    .Verify(serviceCollection => serviceCollection.Add(
        //        It.Is<ServiceDescriptor>(serviceDescriptor => serviceDescriptor.Is<TService, TInstance>(lifetime))));
    }
}