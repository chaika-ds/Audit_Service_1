using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Localization.Localizer.Models;
using AuditService.Localization.Settings;
using KIT.NLog.Extensions;
using Microsoft.Extensions.Logging;
using Tolar.Redis;

namespace AuditService.Localization.Localizer.Storage;

/// <summary>
///     Storage for localization resources.
///     Caching in Redis is used as storage.
/// </summary>
internal class RedisCacheStorage : ILocalizationStorage
{
    private readonly ILogger<RedisCacheStorage> _logger;
    private readonly IRedisCacheStorageSettings _redisCacheStorageSettings;
    private readonly IRedisRepository _redisRepository;

    public RedisCacheStorage(IRedisRepository redisRepository, ILogger<RedisCacheStorage> logger, IRedisCacheStorageSettings redisCacheStorageSettings)
    {
        _redisRepository = redisRepository;
        _logger = logger;
        _redisCacheStorageSettings = redisCacheStorageSettings;
    }

    /// <summary>
    ///     Get localization resources from storage
    /// </summary>
    /// <param name="resourceParameters">Localization resource parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Localization resources</returns>
    public async Task<IDictionary<string, string>> GetResources(LocalizationResourceParameters resourceParameters, CancellationToken cancellationToken)
    {
        try
        {
            var resources = await _redisRepository.GetAsync<Dictionary<string, string>>(GenerateCacheKey(resourceParameters));
            return resources ?? new Dictionary<string, string>();
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Getting cache for localization failed", resourceParameters);
            return new Dictionary<string, string>();
        }
    }

    /// <summary>
    ///     Set localization resources to storage
    /// </summary>
    /// <param name="localizationResources">Localization resources for store in storage</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task execution result</returns>
    public async Task SetResources(LocalizationResources localizationResources, CancellationToken cancellationToken)
    {
        try
        {
            var cacheKey = GenerateCacheKey(localizationResources.ResourceParameters);
            await _redisRepository.SetAsync(cacheKey, localizationResources.Resources, TimeSpan.FromMinutes(_redisCacheStorageSettings.CacheLifetimeInMinutes));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Setting localization resources failed", localizationResources);
        }
    }

    /// <summary>
    ///     Generate a key to store the cache
    /// </summary>
    /// <param name="resourceParameters">Localization resource parameters</param>
    /// <returns>Key to store the cache</returns>
    private static string? GenerateCacheKey(LocalizationResourceParameters resourceParameters)
    {
        var cacheKey = $"Localization_{resourceParameters.Service}_{resourceParameters.Language}";
        return cacheKey.GetHash(HashType.MD5);
    }
}