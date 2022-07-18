using bgTeam.Extensions;
using KIT.Redis.Settings;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Redis;

namespace AuditService.Tests.AuditService.WebApi.Wrapper
{
    public class ServiceCollectionExtensionsWrapper
    {
        private IServiceCollectionExtensionsWrapper _wrapper;

        public ServiceCollectionExtensionsWrapper(IServiceCollectionExtensionsWrapper wrapper)
        {
            _wrapper = wrapper;
        }   

        public IServiceCollection AddSettings<TService, TImpl>(IServiceCollection services, Type[] tServices = null)
            where TService : class
            where TImpl : class, TService
        {

            var value = _wrapper.AddSettings<IRedisSettings, RedisSettings>(services);
            return new ServiceCollection();

            //services.CheckNull(nameof(services));
            //if (!services.Any<ServiceDescriptor>((Func<ServiceDescriptor, bool>)(x => x.ServiceType == typeof(TImpl))))
            //    services.AddSingleton<TImpl>();

            //foreach (Type tService in tServices)
            //    ServiceCollectionServiceExtensions.AddSingleton(services, tService, (Func<IServiceProvider, object>)(x => (object)x.GetService<TImpl>()));
            //return services;
        }



    }
}
