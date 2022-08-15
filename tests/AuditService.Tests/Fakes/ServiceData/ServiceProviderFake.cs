using AuditService.Common.Contexts;
using AuditService.SettingsService.Commands.BaseEntities;
using AuditService.SettingsService.Commands.GetRootNodeTree;
using AuditService.Setup.AppSettings;
using AuditService.Tests.Fakes.SettingsService;
using AuditService.Tests.Fakes.Setup;
using AuditService.Tests.Fakes.Setup.ELK;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Redis;
using static AuditService.Handlers.DiConfigure;

namespace AuditService.Tests.Fakes.ServiceData
{
    /// <summary>
    ///     Provider for fake service providers
    /// </summary>
    internal static class ServiceProviderFake
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

            services.AddScoped(_ => ElasticSearchClientProviderFake.GetFakeElasticSearchClient<T>(jsonContent, index));

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        /// <summary>
        ///     Get service provider for reference request
        /// </summary>
        /// <returns>Service provider</returns>
        internal static IServiceProvider GetServiceProviderForReferenceRequestHandler()
        {
            var services = new ServiceCollection();

            RegistrationServices(services);

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
            services.AddSingleton<IRedisRepository, RedisReposetoryForCachePipelineBehaviorFake>();
            services.AddScoped<IElasticIndexSettings, ElasticSearchSettingsFake>();
            services.AddScoped<IGetRootNodeTreeCommand, GetRootNodeTreeCommandFake>();
            services.AddScoped<SettingsServiceCommands>();
            services.AddTransient(_ => new RequestContext
            {
                Language = "en", 
                XNodeId = Guid.NewGuid().ToString()
            });
            services.AddLogging();
        }
    }
}