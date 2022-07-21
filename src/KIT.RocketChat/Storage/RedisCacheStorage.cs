using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using KIT.NLog.Extensions;
using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using KIT.RocketChat.Commands.PostBufferedTextMessage.Models;
using KIT.RocketChat.Settings.Interfaces;
using Microsoft.Extensions.Logging;
using Tolar.Redis;

namespace KIT.RocketChat.Storage;

/// <summary>
///     RocketChat data storage.
///     Caching in Redis is used as storage.
/// </summary>
internal class RedisCacheStorage : IRocketChatStorage
{
    private readonly ILogger<RedisCacheStorage> _logger;
    private readonly IRocketChatStorageSettings _rocketChatStorageSettings;
    private readonly IRedisRepository _redisRepository;

    public RedisCacheStorage(IRedisRepository redisRepository, ILogger<RedisCacheStorage> logger, IRocketChatStorageSettings rocketChatStorageSettings)
    {
        _redisRepository = redisRepository;
        _logger = logger;
        _rocketChatStorageSettings = rocketChatStorageSettings;
    }

    /// <summary>
    ///     Get authentication data
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Authentication data</returns>
    public async Task<AuthData?> GetAuthData(CancellationToken cancellationToken = default)
    {
        try
        {
            var resources = await _redisRepository.GetAsync<AuthData>(CreateKeyForAuthData());
            return resources;
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Getting AuthData from cache failed");
            return null;
        }
    }

    /// <summary>
    ///     Set authentication data
    /// </summary>
    /// <param name="authData">Authentication data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result</returns>
    public async Task SetAuthData(AuthData authData, CancellationToken cancellationToken = default)
    {
        try
        {
            await _redisRepository.SetAsync(CreateKeyForAuthData(), authData, TimeSpan.FromHours(_rocketChatStorageSettings.AuthDataLifetimeInHours!.Value));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Setting AuthData to cache failed", authData);
        }
    }

    /// <summary>
    ///     Get buffered message
    /// </summary>
    /// <param name="bufferKey">Buffer key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Buffered message</returns>
    public async Task<BufferedMessage?> GetBufferedMessage(string bufferKey, CancellationToken cancellationToken = default)
    {
        try
        {
            var resources = await _redisRepository.GetAsync<BufferedMessage>(CreateKeyForChatBufferKey(bufferKey));
            return resources;
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Getting BufferedMessage from cache failed", bufferKey);
            return null;
        }
    }

    /// <summary>
    ///     Set buffered message
    /// </summary>
    /// <param name="bufferedMessage">Buffered message</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result</returns>
    public async Task SetBufferedMessage(BufferedMessage bufferedMessage, CancellationToken cancellationToken = default)
    {
        try
        {
            await _redisRepository.SetAsync(CreateKeyForChatBufferKey(bufferedMessage.BufferKey), bufferedMessage,
                TimeSpan.FromSeconds(_rocketChatStorageSettings.MessageBufferingTimeInMinutes!.Value));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Setting BufferedMessage to cache failed", bufferedMessage);
        }
    }

    /// <summary>
    ///     Create key to auth data(Redis cache key)
    /// </summary>
    /// <returns>Redis cache key</returns>
    private static string CreateKeyForAuthData() => "RocketChatAuthData".GetHash(HashType.MD5)!;

    /// <summary>
    ///     Create key to chat buffer key(Redis cache key)
    /// </summary>
    /// <param name="bufferKey">Buffer key</param>
    /// <returns>Redis cache key</returns>
    private static string CreateKeyForChatBufferKey(string bufferKey) => $"RocketChatBufferKey{bufferKey}".GetHash(HashType.MD5)!;
}