using AuditService.Providers.Implementations;
using AuditService.Providers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

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

        services.AddHostedService<PermissionPusherProvider>();
    }
}