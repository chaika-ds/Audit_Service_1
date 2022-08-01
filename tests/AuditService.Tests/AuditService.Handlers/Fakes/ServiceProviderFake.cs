using AuditService.Common.Helpers;
using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Handlers;
using AuditService.Setup.AppSettings;
using AuditService.Tests.AuditService.GetAuditLog.Models;
using AuditService.Tests.Factories.Fakes;
using AuditService.Tests.Resources;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Redis;
using static AuditService.Tests.AuditService.Handlers.Fakes.LogRequestBaseHandlerResponsesFake;

namespace AuditService.Tests.AuditService.Handlers.Fakes
{
    internal class ServiceProviderFake
    {
        internal static IServiceProvider CreateElkServiceProviderFake()
        {
            var playerChangesLogDomainModel =
                JsonHelper.ObjectToByteArray(GetTestPlayerChangesLogDomainModelResponse());
            var services = new ServiceCollection();

            DiConfigure.RegisterServices(services);
            services.AddSingleton<IRedisRepository, FakeRedisReposetoryForCachePipelineBehavior>();
            services.AddScoped<IElasticIndexSettings, FakeElasticSearchSettings>();
            services.AddLogging();
            services.AddScoped(_ => FakeElasticSearchClientProvider.GetFakeElasticSearchClient<PlayerChangesLogDomainModel>(playerChangesLogDomainModel));

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
