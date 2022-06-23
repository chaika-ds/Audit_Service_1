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
        RedisConnectionString = configuration["Redis:ConnectionString"];
        RedisPrefix = configuration["Redis:InstanceName"];
    }
}