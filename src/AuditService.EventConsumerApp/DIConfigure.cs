using AuditService.Kafka.Services;
using AuditService.Kafka.Services.ExternalConnectionServices;
using AuditService.Data.Domain.Domain;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Authenticate.Impl;
using AuditService.Kafka.Settings;
using AuditService.Kafka.Services.Health;
using Tolar.Kafka;

namespace AuditService.EventConsumerApp;

/// <summary>
/// DI configuration for Kafka consumer
/// </summary>
public static class DiConfigure
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSettings<IKafkaConsumerSettings, IHealthSettings, IAuthenticateServiceSettings, AppSettings>();

        services
            .AddSingleton(services)
            .AddSingleton<HealthService>()
            .AddSingleton<IHealthMarkService>(x => x.GetRequiredService<HealthService>())
            .AddSingleton<IInputSettings<AuditLogTransactionDomainModel>, InputSettings<AuditLogTransactionDomainModel>>()
            .AddSingleton<IKafkaConsumerFactory, Kafka.Kafka.KafkaConsumerFactory>()
            .AddSingleton<IInputService, InputAuditServiceTransactions>();

        services
            .AddHostedService<InputServicesManager>();

        return services;
    }
}