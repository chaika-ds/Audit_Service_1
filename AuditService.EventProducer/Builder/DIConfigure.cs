using AuditService.Common.Health;
using AuditService.Common.Kafka;
using AuditService.Common.Services;
using AuditService.Common.Services.ExternalConnectionServices;
using AuditService.Data.Domain.Dto;
using AuditService.IntegrationTests.Settings;
using bgTeam.DataAccess;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.EventProducer
{
    public static class DIConfigure
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSettings<IDirectorSettings, IKafkaConsumerSettings,  AppSettings(services)>();

            services
                .AddSingleton(services)
                .AddSingleton<HealthService>()
                .AddSingleton<IHealthMarkService>(x => x.GetRequiredService<HealthService>())
                .AddSingleton<IKafkaConsumerFactory, KafkaConsumerFactory>()

                .AddSingleton<IInputSettings<AuditLogMessageDto>, InputSettings<AuditLogMessageDto>>()
                .AddSingleton<IInputService, InputAuditServiceTransactions>();

            services
                .AddHostedService<InputServicesManager>();

            return services;
        }
    }
}
