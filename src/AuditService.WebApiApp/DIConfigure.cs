using AuditService.Common.Health;
using AuditService.Common.Kafka;
using AuditService.WebApiApp.Services;
using AuditService.WebApiApp.Services.Interfaces;
using bgTeam.DataAccess;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.WebApiApp;

public static class DiConfigure
{
    /// <summary>
    ///     Register custom services
    /// </summary>
    public static void Configure(IServiceCollection services)
    {
        services.AddTransient<IAuditLog, AuditLogService>();
        services.AddTransient<IAuthorization, AuthorizationService>();
        services.AddTransient<IHealthCheck, HealthCheckService>();

        services.AddSettings<IConnectionSetting, IKafkaConsumerSettings, IHealthSettings, AppSettings>();
        
        services
            .AddSingleton(services)
            .AddSingleton<HealthService>()
            .AddSingleton<IHealthMarkService>(x => x.GetRequiredService<HealthService>())
            .AddSingleton<IHealthService>(x => x.GetRequiredService<HealthService>());
    }
}