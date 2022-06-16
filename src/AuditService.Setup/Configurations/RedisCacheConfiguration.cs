using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Setup.Configurations;

/// <summary>
///     Configuration of Redis
/// </summary>
public static class RedisCacheConfiguration
{
    /// <summary>
    ///     Create scope for Redis
    /// </summary>
    public static void AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["RedisCache:ConnectionString"];
            options.InstanceName = configuration["RedisCache:InstanceName"] ?? "RedisCache";
        });
    }
}