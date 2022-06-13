using AuditService.Common.Health;
using AuditService.Common.Kafka;
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
                IKafkaConsumerSettings,
                IHealthSettings,
                IJsonData,
                IAuthenticateServiceSettings,
                IElasticIndex,
                AppSetting>()
            .AddSingleton(services);
        
        services.AddScoped<IReferenceService, ReferenceService>();
        services.AddScoped<IAuditLogService, AuditLogService>();
        services.AddScoped<IHealthCheck, HealthCheckService>();

        services.AddHttpClient<IAuthenticateService, AuthenticateService>();

        services
            .AddSingleton<HealthService>()
            .AddSingleton<IHealthMarkService>(x => x.GetRequiredService<HealthService>())
            .AddSingleton<IHealthService>(x => x.GetRequiredService<HealthService>());
    }
}