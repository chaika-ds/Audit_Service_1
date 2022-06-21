using Microsoft.Extensions.Configuration;
using Tolar.Redis;

namespace AuditService.Setup.AppSettings;

internal class RedisSettings : IRedisSettings
{
    public RedisSettings(IConfiguration configuration) => ApplySettings(configuration);

    public string? RedisConnectionString { get; private set; }

    public string? RedisPrefix { get; private set; }

    /// <summary>
    ///     Apply settings
    /// </summary>
    private void ApplySettings(IConfiguration configuration)
    {
        RedisConnectionString = configuration["REDIS:REDIS_CONNECTION_URL"];
        RedisPrefix = configuration["REDIS:REDIS_PREFIX"];
    }
}