using Microsoft.Extensions.DependencyInjection;
using Tolar.Redis;

namespace AuditService.Setup.ServiceConfigurations;

/// <summary>
///     Configuration of Redis
/// </summary>
public static class RedisCacheConfiguration
{
    /// <summary>
    ///     Create scope for Redis
    /// </summary>
    public static void AddRedisCache(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var settings = serviceProvider.GetRequiredService<IRedisSettings>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = settings.RedisConnectionString;
            options.InstanceName = settings.RedisPrefix;
        });
    }
}