namespace KIT.RocketChat.Commands.PostBufferedTextMessage.Models;

/// <summary>
///     Buffered message
/// </summary>
public class BufferedMessage
{
    public BufferedMessage(string messageId, string bufferKey)
    {
        MessageId = messageId;
        BufferKey = bufferKey;
    }

    /// <summary>
    ///     Message Id
    /// </summary>
    public string MessageId { get; set; }

    /// <summary>
    ///     Buffer key
    /// </summary>
    public string BufferKey { get; set; }
}