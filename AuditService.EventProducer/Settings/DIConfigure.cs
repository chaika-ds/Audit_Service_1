using AuditService.Common.Health;
using AuditService.Common.Kafka;
using AuditService.Common.Services;
using AuditService.Common.Services.ExternalConnectionServices;
using AuditService.Data.Domain.Dto;
using AuditService.IntegrationTests.Settings;
using AuditService.Test.EventProducer.Builder;
using bgTeam.DataAccess;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;


namespace AuditService.IntegrationTests.Settings
{
    internal static class DiConfigure
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSettings<IDirectorSettings,IKafkaConsumerSettings, AppSettings>();

            services.AddSingleton<KafkaProducer>();

            services
                .AddSingleton(services)
                .AddSingleton<IBuilderDto<AuditLogMessageDto>, BuilderBase<AuditLogMessageDto>>();




            return services;
        }
        public void ServiceConfiguration()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddServices();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var service = serviceProvider.GetService<RequestHandler>();
        }
    }
}
