using AuditService.Common.Helpers;
using AuditService.Handlers;
using AuditService.Setup.AppSettings;
using AuditService.Tests.AuditService.GetAuditLog.Models;
using AuditService.Tests.Resources;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Redis;
using static AuditService.Tests.AuditService.Handlers.Fakes.LogRequestBaseHandlerResponsesFake;

namespace AuditService.Tests.Factories.Fakes
{
    internal class ServiceProviderFake
    {
        internal static IServiceProvider CreateElkServiceProviderFake<TLogType>(byte[] elasticClientJsonContent)
        {
            var services = new ServiceCollection();

            DiConfigure.RegisterServices(services);
            services.AddSingleton<IRedisRepository, FakeRedisReposetoryForCachePipelineBehavior>();
            services.AddScoped<IElasticIndexSettings, FakeElasticSearchSettings>();
            services.AddLogging();
            services.AddScoped(_ => FakeElasticSearchClientProvider.GetFakeElasticSearchClient<TLogType>(elasticClientJsonContent, TestResources.PlayerChangesLog));

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
