using AuditService.Kafka.AppSetings;
using AuditService.Setup.AppSettings;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Authenticate.Impl;
using Tolar.Kafka;
using Tolar.Redis;

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
        services.AddSettings<IRedisSettings, RedisSettings>();
        services.AddSettings<IKafkaSettings, KafkaSettings>();
        services.AddSettings<IHealthSettings, HealthSettings>();
        services.AddSettings<IAuthenticateServiceSettings, AuthSsoServiceSettings>();
        services.AddSettings<IAuthSsoServiceSettings, AuthSsoServiceSettings>();
        services.AddSettings<IPermissionPusherSettings, PermissionPusherSettings>();
        services.AddSettings<IElasticSearchSettings, ElasticSearchSettings>();
        services.AddSettings<IElasticIndexSettings, ElasticSearchSettings>();
        services.AddSettings<ISwaggerSettings, SwaggerSettings>();
    }
}