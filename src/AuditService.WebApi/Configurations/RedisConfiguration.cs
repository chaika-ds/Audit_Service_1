namespace AuditService.WebApi.Configurations;

public static class RedisConfiguration
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