using AuditService.Common.Health;
using AuditService.Common.Kafka;
using AuditService.Common.Services;
using AuditService.Common.Services.ExternalConnectionServices;
using AuditService.Data.Domain.Dto;
using bgTeam.DataAccess;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.EventConsumerApp
{
    public static class DIConfigure
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSettings<IConnectionSetting, IKafkaConsumerSettings, IHealthSettings, AppSettings>();

            services
                .AddSingleton(services)
                .AddSingleton<HealthService>()
                .AddSingleton<IHealthMarkService>(x => x.GetRequiredService<HealthService>())
                .AddSingleton<IKafkaConsumerFactory, KafkaConsumerFactory>()

                .AddSingleton<IInputSettings<AuditLogTransactionDto>, InputSettings<AuditLogTransactionDto>>()
                .AddSingleton<IInputService, InputAuditServiceTransactions>();

            services
                .AddHostedService<InputServicesManager>();

            return services;
        }
    }
}
