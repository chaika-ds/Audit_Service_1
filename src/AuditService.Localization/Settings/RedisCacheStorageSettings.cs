using Microsoft.Extensions.Configuration;

namespace AuditService.Localization.Settings;

/// <summary>
///     Redis storage settings for storing localization resources
/// </summary>
internal class RedisCacheStorageSettings : IRedisCacheStorageSettings
{
    public RedisCacheStorageSettings(IConfiguration configuration) => ApplySettings(configuration);

    /// <summary>
    ///     Cache lifetime in minutes
    /// </summary>
    public int CacheLifetimeInMinutes { get; set; }

    /// <summary>
    ///     Apply settings
    /// </summary>
    private void ApplySettings(IConfiguration configuration) => CacheLifetimeInMinutes = Convert.ToInt32(configuration["Localization:Storage:CacheLifetimeInMinutes"]);
}