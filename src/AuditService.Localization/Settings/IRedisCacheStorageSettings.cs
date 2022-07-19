namespace AuditService.Localization.Settings;

/// <summary>
///     Redis storage settings for storing localization resources
/// </summary>
public interface IRedisCacheStorageSettings
{
    /// <summary>
    ///     Cache lifetime in minutes
    /// </summary>
    int CacheLifetimeInMinutes { get; set; }
}