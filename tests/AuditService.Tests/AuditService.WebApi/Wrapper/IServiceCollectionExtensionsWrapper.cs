using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.AuditService.WebApi.Wrapper
{
    public interface IServiceCollectionExtensionsWrapper
    {
        public IServiceCollection AddSettings<TService, TImpl>(IServiceCollection services, Type[] tServices = null) where TService : class
            where TImpl : class, TService;
    }
}
