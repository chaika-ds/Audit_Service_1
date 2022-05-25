using AuditService.WebApi.Services;
using AuditService.WebApi.Services.Interfaces;
using Elasticsearch.Net;
using Nest;

namespace AuditService.WebApi;

public static class DiConfigure
{
    public static void Configure(IServiceCollection services)
    {
        services.AddScoped<IAuditLog, AuditLogService>();
    }
}