using Microsoft.OpenApi.Models;

namespace AuditService.WebApi.Configurations;

public static class SwaggerConfiguration
{
    public static void Configure(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuditService.Web", Version = "v1" });
            c.DescribeAllParametersInCamelCase();
            c.CustomSchemaIds(x => x.FullName);
        });
    }
    
    public static void UseConfigure(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuditService.WebApi v1"));
    }
}