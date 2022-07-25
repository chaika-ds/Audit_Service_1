using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using KIT.RocketChat.Commands.PostBufferedTextMessage.Models;

namespace KIT.RocketChat.Storage;

/// <summary>
///     RocketChat data storage
/// </summary>
public interface IRocketChatStorage
{
    /// <summary>
    ///     Get authentication data
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Authentication data</returns>
    Task<AuthData?> GetAuthData(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Set authentication data
    /// </summary>
    /// <param name="authData">Authentication data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result</returns>
    Task SetAuthData(AuthData authData, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get buffered message
    /// </summary>
    /// <param name="bufferKey">Buffer key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Buffered message</returns>
    Task<BufferedMessage?> GetBufferedMessage(string bufferKey, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Set buffered message
    ///     When specifying the "IsBlockedMessage" parameter - blocks the message
    /// </summary>
    /// <param name="bufferedMessage">Buffered message</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result</returns>
    Task SetBufferedMessage(BufferedMessage bufferedMessage, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Set a block on a buffered message
    /// </summary>
    /// <param name="messageId">Message Id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result</returns>
    Task SetBlockOnBufferedMessage(string messageId, CancellationToken cancellationToken = default);
}