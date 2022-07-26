namespace KIT.RocketChat.Commands.PostBufferedTextMessage.Models;

/// <summary>
///     Buffered message
/// </summary>
public class BufferedMessage
{
    public BufferedMessage(string messageId, string bufferKey, bool isBlockedMessage = true)
    {
        MessageId = messageId;
        BufferKey = bufferKey;
        IsBlockedMessage = isBlockedMessage;
    }

    /// <summary>
    ///     Message Id
    /// </summary>
    public string MessageId { get; set; }

    /// <summary>
    ///     Buffer key
    /// </summary>
    public string BufferKey { get; set; }

    /// <summary>
    ///     Buffered message blocked
    /// </summary>
    public bool IsBlockedMessage { get; set; }
}