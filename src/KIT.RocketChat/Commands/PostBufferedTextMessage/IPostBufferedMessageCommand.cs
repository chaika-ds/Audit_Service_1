using KIT.RocketChat.Commands.PostBufferedTextMessage.Models;

namespace KIT.RocketChat.Commands.PostBufferedTextMessage;

/// <summary>
///     RocketChat command to post buffered message
/// </summary>
public interface IPostBufferedMessageCommand
{
    /// <summary>
    ///     Execute a command
    /// </summary>
    /// <param name="request">Request model for post a buffered message in a thread</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result</returns>
    Task<bool> Execute(PostBufferedMessageRequest request, CancellationToken cancellationToken);
}