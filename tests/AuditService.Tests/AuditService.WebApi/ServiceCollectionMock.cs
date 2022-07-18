using AuditService.Tests.AuditService.WebApi.HttpClientMock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AuditService.Tests.AuditService.WebApi
{
    public sealed class ServiceCollectionMock
    {
        public readonly ServiceCollection ServiceCollectionTest;

        internal ServiceCollectionVerifier ServiceCollectionVerifier;

        public ServiceCollectionMock()
        {
            ServiceCollectionTest = new ServiceCollection();
            ServiceCollectionTest.AddScoped<IConfiguration, ConfigurationFake>();

            ServiceCollectionVerifier = new ServiceCollectionVerifier(ServiceCollectionTest);
        }

        public IServiceCollection ServiceCollection => ServiceCollectionTest;

        public void ContainsSingletonService<TService, TInstance>()
        {
            ServiceCollectionVerifier.ContainsSingletonService<TService, TInstance>();
        }

        public void ContainsTransientService<TService, TInstance>() 
        {
            ServiceCollectionVerifier.ContainsTransientService<TService, TInstance>();
        }

        public void ContainsScopedService<TService, TInstance>()
        {
            ServiceCollectionVerifier.ContainsTransientService<TService, TInstance>();
        }
    }
}
