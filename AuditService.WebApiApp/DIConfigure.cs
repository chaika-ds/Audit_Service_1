using AuditService.WebApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.WebApiApp;
public static class DIConfigure
{
    public static void Configure(IServiceCollection services)
    {
        services.AddScoped<IAuditLog, AuditLogService>();
    }
}