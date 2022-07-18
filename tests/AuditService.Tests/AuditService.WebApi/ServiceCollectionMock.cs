using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AuditService.Tests.AuditService.WebApi
{
    public sealed class ServiceCollectionMock
    {
        public readonly Mock<ServiceCollection> _serviceCollectionMock;
        private readonly ServiceCollectionVerifier _serviceCollectionVerifier;

        public ServiceCollectionMock()
        {
            _serviceCollectionMock = new Mock<ServiceCollection>();

            _serviceCollectionVerifier = new ServiceCollectionVerifier();
        }

        public IServiceCollection ServiceCollection => _serviceCollectionMock.Object;

        public ServiceCollectionVerifier ServiceCollectionVerifier { get; set; }

        public void ContainsSingletonService<TService, TInstance>()
        {
            _serviceCollectionVerifier.ContainsSingletonService<TService, TInstance>();
        }

        public void ContainsTransientService<TService, TInstance>()
        {
            _serviceCollectionVerifier.ContainsTransientService<TService, TInstance>();
        }

        public void ContainsScopedService<TService, TInstance>()
        {
            _serviceCollectionVerifier.ContainsTransientService<TService, TInstance>();
        }

        public IServiceCollection AddSettings<TService, TImpl>()
        {
            return null;
        }
    }
}
