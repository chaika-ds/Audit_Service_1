using AuditService.SettingsService.Settings.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AuditService.SettingsService.Settings;

/// <summary>
///     Redis storage settings for storing settings service resources
/// </summary>
internal class RedisCacheStorageSettings : IRedisCacheStorageSettings
{
    public RedisCacheStorageSettings(IConfiguration configuration)
    {
        CacheLifetimeInMinutes = int.Parse(configuration["SettingsService:Storage:CacheLifetimeInMinutes"]);
    }

    /// <summary>
    ///     Cache lifetime in minutes
    /// </summary>
    public int CacheLifetimeInMinutes { get; set; }
}