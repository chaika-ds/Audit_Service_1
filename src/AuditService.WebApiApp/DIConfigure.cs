using AuditService.Kafka.Kafka;
using AuditService.Utility.Logger;
using AuditService.Kafka.Settings;
using AuditService.WebApiApp.Services;
using AuditService.WebApiApp.Services.Interfaces;
using bgTeam.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Authenticate;
using Tolar.Authenticate.Impl;
using Tolar.Kafka;
using AuditService.Kafka.Services.Health;

namespace AuditService.WebApiApp;

public static class DiConfigure
{
    /// <summary>
    ///     Register custom services
    /// </summary>
    public static void Configure(IServiceCollection services)
    {
        services.AddTransient<IAuditLog, AuditLogService>();
        services.AddTransient<IHealthCheck, HealthCheckService>();
        
        services
            .AddSettings<
                Kafka.Settings.IKafkaSettings,
                IHealthSettings,
                IJsonData,
                IAuthenticateServiceSettings,
                IPermissionPusherSettings,
                AppSettings>()
            .AddSingleton(services)
            .AddSingleton<IKafkaProducer, Kafka.Kafka.KafkaProducer>();
        
        services.AddScoped<IReferenceProvider, ReferenceProvider>();
        
        services.AddHttpClient<IAuthenticateService, AuthenticateService>();

        services
            .AddSingleton<HealthService>()
            .AddSingleton<IHealthMarkService>(x => x.GetRequiredService<HealthService>())
            .AddSingleton<IHealthService>(x => x.GetRequiredService<HealthService>());

        services.AddHostedService<PermissionPusherImpl>();
    }
}