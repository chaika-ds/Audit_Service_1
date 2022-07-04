using AuditService.Kafka.AppSetings;
using AuditService.Kafka.Services;
using AuditService.Kafka.Services.ExternalConnectionServices;
using AuditService.Kafka.Services.Health;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Kafka;

namespace AuditService.EventConsumer;

/// <summary>
///     DI configuration for Kafka consumer
/// </summary>
public static class DiConfigure
{
    public static void AddKafkaServices(this IServiceCollection services)
    {
        services.AddSettings<IKafkaTopics, KafkaTopics>();

        services
            .AddSingleton<HealthService>()
            .AddSingleton<IHealthMarkService>(x => x.GetRequiredService<HealthService>())
            .AddSingleton<IKafkaConsumerFactory, KafkaConsumerFactory>()
            .AddSingleton<IInputService, InputAuditServiceTransactions>()
            .AddSingleton<IKafkaProducer, KafkaProducer>();

        services
            .AddHostedService<InputServicesManager>();
    }
}