using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.SettingsService.ApiClient.Models;
using AuditService.SettingsService.Settings.Interfaces;
using KIT.NLog.Extensions;
using Microsoft.Extensions.Logging;
using Tolar.Redis;

namespace AuditService.SettingsService.Storage;

/// <summary>
///     Storage for settings service resources.
///     Caching in Redis is used as storage.
/// </summary>
internal class RedisCacheStorage : ISettingsServiceStorage
{
    private readonly IRedisRepository _redisRepository;
    private readonly ILogger<RedisCacheStorage> _logger;
    private readonly IRedisCacheStorageSettings _redisCacheStorageSettings;

    public RedisCacheStorage(IRedisRepository redisRepository, ILogger<RedisCacheStorage> logger, IRedisCacheStorageSettings redisCacheStorageSettings)
    {
        _redisRepository = redisRepository;
        _logger = logger;
        _redisCacheStorageSettings = redisCacheStorageSettings;
    }

    /// <summary>
    ///     Get root node tree
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Root node tree</returns>
    public async Task<NodeModel?> GetRootNodeTree(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _redisRepository.GetAsync<NodeModel>(GenerateCacheKeyForNodeStorage());
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Getting RootNodeTree from cache failed");
            return null;
        }
    }

    /// <summary>
    ///     Set root node tree
    /// </summary>
    /// <param name="model">Root node tree</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task execution result</returns>
    public async Task SetRootNodeTree(NodeModel model, CancellationToken cancellationToken = default)
    {
        try
        {
            await _redisRepository.SetAsync(GenerateCacheKeyForNodeStorage(), model, TimeSpan.FromMinutes(_redisCacheStorageSettings.CacheLifetimeInMinutes));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Setting RootNodeTree failed", model);
        }
    }

    /// <summary>
    ///     Generate a key to store the cache node
    /// </summary>
    /// <returns>Key to store the cache</returns>
    private static string? GenerateCacheKeyForNodeStorage() =>
        "AuditService.SettingsService.RootNodeTree".GetHash(HashType.MD5);
}