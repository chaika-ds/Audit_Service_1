using AuditService.Kafka.Settings;
using AuditService.WebApiApp.AppSettings;
using AuditService.WebApiApp.Providers;
using AuditService.WebApiApp.Services;
using AuditService.WebApiApp.Services.Interfaces;
using bgTeam.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
        services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ProduceResponseTypeModelProvider>());
        
        services
            .AddSettings<
                Kafka.Settings.IKafkaSettings,
                IHealthSettings,
                IJsonData,
                IAuthenticateServiceSettings,
                IPermissionPusherSettings,
                IElasticIndex,
                AppSetting >()
            .AddSingleton(services)
            .AddSingleton<IKafkaProducer, Kafka.Kafka.KafkaProducer>();
        
        services.AddScoped<IReferenceService, ReferenceService>();
        services.AddScoped<IAuditLogService, AuditLogService>();
        services.AddScoped<IHealthCheck, HealthCheckService>();

        services.AddHttpClient<IAuthenticateService, AuthenticateService>();

        services
            .AddSingleton<HealthService>()
            .AddSingleton<IHealthMarkService>(x => x.GetRequiredService<HealthService>())
            .AddSingleton<IHealthService>(x => x.GetRequiredService<HealthService>());

        services.AddHostedService<PermissionPusherImpl>();
    }
}