namespace AuditService.SettingsService.Settings.Interfaces;

/// <summary>
///     Redis storage settings for storing settings service resources
/// </summary>
public interface IRedisCacheStorageSettings
{
    /// <summary>
    ///     Cache lifetime in minutes
    /// </summary>
    int CacheLifetimeInMinutes { get; set; }
}