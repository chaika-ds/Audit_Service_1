using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using KIT.RocketChat.Commands.PostBufferedTextMessage.Models;
using KIT.RocketChat.Storage;

namespace AuditService.Tests.Fakes.RocketChat
{
    /// <summary>
    ///     RocketChatStorag Fake
    /// </summary>
    internal class RocketChatStorageFake : IRocketChatStorage
    {
        /// <summary>
        ///     Fake text value
        /// </summary>
        private const string fakeTextValue = "test";

        /// <summary>
        ///     Get authentication data fake
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Authentication data</returns>
        public Task<AuthData?> GetAuthData(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new AuthData(fakeTextValue, fakeTextValue));
        }

        /// <summary>
        ///     Get buffered message fake
        /// </summary>
        /// <param name="bufferKey">Buffer key</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Buffered message</returns>
        public Task<BufferedMessage?> GetBufferedMessage(string bufferKey, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new BufferedMessage(fakeTextValue, bufferKey, false));
        }

        /// <summary>
        ///     Get buffered message fake
        /// </summary>
        /// <param name="bufferKey">Buffer key</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Buffered message</returns>
        public Task SetAuthData(AuthData authData, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        ///     Set a block on a buffered message fake
        /// </summary>
        /// <param name="messageId">Message Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Execution result</returns>
        public Task SetBlockOnBufferedMessage(string messageId, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        ///     Set buffered message fake
        /// </summary>
        /// <param name="bufferedMessage">Buffered message</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Execution result</returns>
        public Task SetBufferedMessage(BufferedMessage bufferedMessage, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }
    }
}
