using AuditService.Localization.Settings;

namespace AuditService.Tests.Fakes.Localization;

/// <summary>
///      RedisCacheStorageSettings fake
/// </summary>
public class RedisCacheStorageSettingsFake: IRedisCacheStorageSettings
{
    /// <summary>
    ///     Cache lifetime in minutes
    /// </summary>
    public int CacheLifetimeInMinutes
    {
        get => 10;
        set => throw new NotImplementedException();
    }
}