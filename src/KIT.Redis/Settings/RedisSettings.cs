﻿using Microsoft.Extensions.Configuration;
using Tolar.Redis;

namespace KIT.Redis.Settings;

/// <summary>
///     Redis service settings
/// </summary>
public class RedisSettings : IRedisSettings
{
    public RedisSettings(IConfiguration configuration)
    {
        ApplySettings(configuration);
    }

    /// <summary>
    ///     Redis connection string
    /// </summary>
    public string? RedisConnectionString { get; private set; }

    /// <summary>
    ///     Special prefix for generating keys
    /// </summary>
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