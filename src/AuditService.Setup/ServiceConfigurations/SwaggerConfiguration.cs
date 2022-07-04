using AuditService.Setup.AppSettings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Tolar.Authenticate;

namespace AuditService.Setup.ServiceConfigurations;

public static class SwaggerConfiguration
{
    /// <summary>
    ///     Create scope for Swagger
    /// </summary>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            var serviceProvider = services.BuildServiceProvider();
            var ssoSettings = serviceProvider.GetRequiredService<IAuthSsoServiceSettings>();
            var swaggerSettings = serviceProvider.GetRequiredService<ISwaggerSettings>();
            var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

            c.SwaggerDoc("v2", new OpenApiInfo { Title = $"{ssoSettings.ServiceName} ({environment.EnvironmentName.ToLower()})", Version = "v2" });
            c.DescribeAllParametersInCamelCase();
            c.UseInlineDefinitionsForEnums();
            c.CustomSchemaIds(x => x.Name);
            
            foreach (var path in swaggerSettings.XmlComments)
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
            c.SwaggerEndpoint("/swagger/v2/swagger.json", $"{app.Services.GetRequiredService<IAuthSsoServiceSettings>().ServiceName} v2");
        });
    }
}