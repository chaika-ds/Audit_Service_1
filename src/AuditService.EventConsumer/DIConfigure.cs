using AuditService.Common.Models.Domain;
using AuditService.Kafka.Services;
using Microsoft.Extensions.DependencyInjection;
using AuditService.Kafka.Services.Health;
using Tolar.Kafka;
using AuditService.Kafka.AppSetings;
using AuditService.Kafka.Services.ExternalConnectionServices;

namespace AuditService.EventConsumer;

/// <summary>
/// DI configuration for Kafka consumer
/// </summary>
public static class DiConfigure
{
    public static void KafkaServices(this IServiceCollection services)
    {
        services
            .AddSingleton(services)
            .AddSingleton<HealthService>()
            .AddSingleton<IHealthMarkService>(x => x.GetRequiredService<HealthService>())
            .AddSingleton<IInputSettings<AuditLogTransactionDomainModel>, InputSettings<AuditLogTransactionDomainModel>>()
            .AddSingleton<IKafkaConsumerFactory, Kafka.Kafka.KafkaConsumerFactory>()
            .AddSingleton<IInputService, InputAuditServiceTransactions>();

        services
            .AddHostedService<InputServicesManager>();
    }
}