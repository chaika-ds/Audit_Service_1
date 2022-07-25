using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using KIT.NLog.Extensions;
using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using KIT.RocketChat.Commands.PostBufferedTextMessage.Models;
using KIT.RocketChat.Settings.Interfaces;
using KIT.RocketChat.Storage.Models;
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
            var authData = await _redisRepository.GetAsync<AuthData>(CreateKeyForAuthData());
            return authData;
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
            var bufferedMessageStoreModel = await _redisRepository.GetAsync<BufferedMessageStoreModel>(CreateKeyForChatBufferKey(bufferKey));

            return bufferedMessageStoreModel == null ? null : 
                new BufferedMessage(bufferedMessageStoreModel.MessageId, bufferedMessageStoreModel.BufferKey, await IsBlockedMessage(bufferedMessageStoreModel.MessageId));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Getting BufferedMessage from cache failed", bufferKey);
            return null;
        }
    }

    /// <summary>
    ///     Set buffered message
    ///     When specifying the "IsBlockedMessage" parameter - blocks the message
    /// </summary>
    /// <param name="bufferedMessage">Buffered message</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result</returns>
    public async Task SetBufferedMessage(BufferedMessage bufferedMessage, CancellationToken cancellationToken = default)
    {
        try
        {
            var bufferedMessageStoreModel = new BufferedMessageStoreModel(bufferedMessage.MessageId, bufferedMessage.BufferKey);

            await _redisRepository.SetAsync(CreateKeyForChatBufferKey(bufferedMessage.BufferKey), bufferedMessageStoreModel,
                TimeSpan.FromMinutes(_rocketChatStorageSettings.BufferedMessageLifetimeInMinutes!.Value));

            if (bufferedMessage.IsBlockedMessage)
                await SetBlockOnBufferedMessage(bufferedMessage.MessageId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Setting BufferedMessage to cache failed", bufferedMessage);
        }
    }

    /// <summary>
    ///     Set a block on a buffered message
    /// </summary>
    /// <param name="messageId">Message Id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result</returns>
    public async Task SetBlockOnBufferedMessage(string messageId, CancellationToken cancellationToken = default)
    {
        try
        {
            var cacheKey = CreateKeyForMessageBlocking(messageId);
            await _redisRepository.DeleteAsync(cacheKey);
            await _redisRepository.SetAsync(cacheKey, new BlockingMessageStoreModel(messageId), 
                TimeSpan.FromMinutes(_rocketChatStorageSettings.BufferedMessageBlockLifetimeInMinutes!.Value));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Block buffered message failed", messageId);
        }
    }
    
    /// <summary>
    ///     Check if a message is blocked
    /// </summary>
    /// <param name="messageId">Message Id</param>
    /// <returns>The message is blocked</returns>
    private async Task<bool> IsBlockedMessage(string messageId)
    {
        try
        {
            return await _redisRepository.GetAsync<BlockingMessageStoreModel>(CreateKeyForMessageBlocking(messageId)) != null;
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Getting BlockingMessage from cache failed", messageId);
            return false;
        }
    }

    /// <summary>
    ///     Create key to message blocking
    /// </summary>
    /// <param name="messageId">Message Id</param>
    /// <returns>Redis cache key</returns>
    private static string CreateKeyForMessageBlocking(string messageId) => $"MessageBlocking_{messageId}".GetHash(HashType.MD5)!;

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