using AuditService.WebApiApp.Services;
using AuditService.WebApiApp.Services.Interfaces;
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
    }
}