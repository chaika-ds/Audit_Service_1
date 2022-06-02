using Microsoft.OpenApi.Models;

namespace AuditService.WebApi.Configurations;

public static class SwaggerConfiguration
{
    /// <summary>
    ///     Create scope for Swagger
    /// </summary>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuditService.Web", Version = "v1" });
            c.DescribeAllParametersInCamelCase();
            c.CustomSchemaIds(x => x.FullName);
        });
    }
    
    /// <summary>
    ///     Use swagger configuration UI
    /// </summary>
    public static void UseSwagger(this WebApplication app)
    {
        SwaggerBuilderExtensions.UseSwagger(app);
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuditService.WebApi v1"));
    }
}