using AuditService.Kafka.Services;
using Microsoft.Extensions.DependencyInjection;
using AuditService.Kafka.Services.Health;
using Tolar.Kafka;
using AuditService.Kafka.AppSetings;
using AuditService.Kafka.Services.ExternalConnectionServices;
using bgTeam.Extensions;

namespace AuditService.EventConsumer;

/// <summary>
/// DI configuration for Kafka consumer
/// </summary>
public static class DiConfigure
{
    public static void KafkaServices(this IServiceCollection services)
    {
        services.AddSettings<IKafkaTopics, KafkaTopics>();

        services
            .AddSingleton<HealthService>()
            .AddSingleton<IHealthMarkService>(x => x.GetRequiredService<HealthService>())
            .AddSingleton<IKafkaConsumerFactory, KafkaConsumerFactory>()
            .AddSingleton<IInputService, InputAuditServiceTransactions>();

        services
            .AddHostedService<InputServicesManager>();
    }
}