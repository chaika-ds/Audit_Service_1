using AuditService.Setup.AppSettings;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Authenticate.Impl;

namespace AuditService.Setup;

/// <summary>
///     Registry of settings
/// </summary>
public static class RegistrySettings
{
    /// <summary>
    ///     Register app settings by sections
    /// </summary>
    public static void RegisterSettings(this IServiceCollection services)
    {
        services.AddSettings<IAuthenticateServiceSettings, AuthSsoServiceSettings>();
        services.AddSettings<IAuthSsoServiceSettings, AuthSsoServiceSettings>();
        services.AddSettings<IElasticSearchSettings, ElasticSearchSettings>();
        services.AddSettings<IElasticIndexSettings, ElasticSearchSettings>();
        services.AddSettings<ISwaggerSettings, SwaggerSettings>();
    }
}