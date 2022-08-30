using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using KIT.RocketChat.Commands.PostBufferedTextMessage.Models;
using KIT.RocketChat.Storage;

namespace AuditService.Tests.Fakes.RocketChat
{
    /// <summary>
    ///     RocketChatStorag Fake
    /// </summary>
    internal class RocketChatStorageThrowExceptionFake : IRocketChatStorage
    {
        /// <summary>
        ///     Get authentication data fake
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Exception</returns>
        public Task<AuthData?> GetAuthData(CancellationToken cancellationToken = default)
        {
            throw new Exception();
        }

        /// <summary>
        ///     Get buffered message fake
        /// </summary>
        /// <param name="bufferKey">Buffer key</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Exception</returns>
        public Task<BufferedMessage?> GetBufferedMessage(string bufferKey, CancellationToken cancellationToken = default)
        {
            throw new Exception();
        }

        /// <summary>
        ///     Get buffered message fake
        /// </summary>
        /// <param name="bufferKey">Buffer key</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Exception</returns>
        public Task SetAuthData(AuthData authData, CancellationToken cancellationToken = default)
        {
            throw new Exception();
        }

        /// <summary>
        ///     Set a block on a buffered message fake
        /// </summary>
        /// <param name="messageId">Message Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Exception</returns>
        public Task SetBlockOnBufferedMessage(string messageId, CancellationToken cancellationToken = default)
        {
            throw new Exception();
        }

        /// <summary>
        ///     Set buffered message fake
        /// </summary>
        /// <param name="bufferedMessage">Buffered message</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Exception</returns>
        public Task SetBufferedMessage(BufferedMessage bufferedMessage, CancellationToken cancellationToken = default)
        {
            throw new Exception();
        }
    }
}
