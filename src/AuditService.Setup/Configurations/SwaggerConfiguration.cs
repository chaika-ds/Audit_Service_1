using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Tolar.Authenticate;

namespace AuditService.Setup.Configurations;

public static class SwaggerConfiguration
{
    /// <summary>
    ///     Create scope for Swagger
    /// </summary>
    public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = configuration["SSO:SSO_SERVICE_NAME"], Version = "v1" });
            c.DescribeAllParametersInCamelCase();
            c.UseInlineDefinitionsForEnums();
            c.CustomSchemaIds(x => x.Name);
            
            var paths = configuration.GetSection("SwaggerXmlComments").Get<string[]>();
            foreach (var path in paths)
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, path), true);

            c.AddSecurityDefinition(
                IAuthenticateService.NODE_ID_KEY,
                new OpenApiSecurityScheme
                {
                    Name = IAuthenticateService.NODE_ID_KEY,
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                });

            c.AddSecurityDefinition(
                IAuthenticateService.TOKEN_KEY,
                new OpenApiSecurityScheme
                {
                    Name = IAuthenticateService.TOKEN_KEY,
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Description = "Copy value from a return sso api /Account/Login or /Account/ServiceLogin.",
                }
            );

            var tokenSecurity = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = IAuthenticateService.TOKEN_KEY }
            };

            var xNodeIdSecurity = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = IAuthenticateService.NODE_ID_KEY }
            };

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                [tokenSecurity] = Array.Empty<string>(),
                [xNodeIdSecurity] = Array.Empty<string>(),
            });
        });
    }
    
    /// <summary>
    ///     Use swagger configuration UI
    /// </summary>
    public static void UseSwagger(this WebApplication app)
    {
        SwaggerBuilderExtensions.UseSwagger(app);
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{app.Configuration["SSO:SSO_SERVICE_NAME"]} v1");
        });
    }
}