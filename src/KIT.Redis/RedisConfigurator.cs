using bgTeam.Extensions;
using KIT.Redis.HealthCheck;
using KIT.Redis.Settings;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Redis;

namespace KIT.Redis;

/// <summary>
///     Redis configurator
/// </summary>
public static class RedisConfigurator
{
    /// <summary>
    ///     Configure Redis. Register services and settings.
    /// </summary>
    /// <param name="services">Services сollection</param>
    public static void ConfigureRedis(this IServiceCollection services)
    {
        services.AddSettings<IRedisSettings, RedisSettings>();
        services.AddSingleton<IRedisRepository, RedisRepository>();
        services.AddRedisCache();
        services.AddSingleton<IRedisHealthCheck, RedisHealthCheck>();
    }

    /// <summary>
    ///     Create scope for Redis
    /// </summary>
    private static void AddRedisCache(this IServiceCollection services)
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