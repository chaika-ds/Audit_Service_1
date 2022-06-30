using AuditService.Providers.Implementations;
using AuditService.Providers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Kafka;

namespace AuditService.Providers;

public static class DiConfigure
{
    /// <summary>
    ///     Register custom services
    /// </summary>
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IReferenceProvider, ReferenceProvider>();
        services.AddScoped<IAuditLogProvider, AuditLogProvider>();
        services.AddScoped<IHealthCheckProvider, HealthCheckProvider>();

        services
            .AddSingleton<IKafkaProducer, KafkaProducer>()
            .AddHostedService<PermissionPusherProvider>();
    }
}