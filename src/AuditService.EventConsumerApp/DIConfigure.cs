using AuditService.Common.Health;
using AuditService.Common.Kafka;
using AuditService.Common.Services;
using AuditService.Common.Services.ExternalConnectionServices;
using AuditService.Data.Domain.Domain;
using AuditService.Data.Domain.Dto;
using bgTeam.DataAccess;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AuditService.EventConsumerApp;

/// <summary>
/// DI configuration for Kafka consumer
/// </summary>
public static class DiConfigure
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSettings<IConnectionSetting, IKafkaConsumerSettings, IHealthSettings, AppSettings>();

        services
            .AddSingleton(services)
            .AddSingleton<HealthService>()
            .AddSingleton<IHealthMarkService>(x => x.GetRequiredService<HealthService>())
            .AddSingleton<IKafkaConsumerFactory, KafkaConsumerFactory>()
            .AddSingleton<IInputSettings<AuditLogTransactionDomainModel>, InputSettings<AuditLogTransactionDomainModel>>()
            .AddSingleton<IInputService, InputAuditServiceTransactions>();

        services
            .AddHostedService<InputServicesManager>();

        return services;
    }
}