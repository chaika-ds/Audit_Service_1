using AuditService.Setup.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Redis;
using static AuditService.Handlers.DiConfigure;

namespace AuditService.Tests.Fakes
{
    /// <summary>
    ///     Provider for fake service providers
    /// </summary>
    internal static class FakeServiceProvider
    {
        /// <summary>
        ///     Get service provider for log handlers
        /// </summary>
        /// <typeparam name="T">type of elk document</typeparam>
        /// <param name="jsonContent">json with content for elk in byte[] formate</param>
        /// <param name="index">elk index</param>
        /// <returns>Service provider</returns>
        internal static IServiceProvider GetServiceProviderForLogHandlers<T>(byte[] jsonContent, string index)
        {
            var services = new ServiceCollection();

            RegistrationServices(services);

            services.AddScoped(serviceProvider => FakeElasticSearchClientProvider.GetFakeElasticSearchClient<T>(jsonContent, index));

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        /// <summary>
        ///     Get service provider for log handlers
        /// </summary>
        /// <param name="index">elk index</param>
        /// <returns>Service provider</returns>
        internal static IServiceProvider GetServiceProviderForLogHandlers(string index)
        {
            var services = new ServiceCollection();

            RegistrationServices(services);

            services.AddScoped(serviceProvider =>
            {
                return FakeElasticSearchClientProvider.GetFakeElasticSearchClient(index);
            });

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        /// <summary>
        ///     Registration default services 
        /// </summary>
        /// <param name="services">service collection</param>
        private static void RegistrationServices(ServiceCollection services)
        {
            RegisterServices(services);
            services.AddSingleton<IRedisRepository, FakeRedisReposetoryForCachePipelineBehavior>();
            services.AddScoped<IElasticIndexSettings, FakeElasticSearchSettings>();
            services.AddLogging();
        }
    }
}
