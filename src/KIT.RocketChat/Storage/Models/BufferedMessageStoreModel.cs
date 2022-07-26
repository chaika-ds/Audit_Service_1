namespace KIT.RocketChat.Storage.Models;

/// <summary>
///     Model store for buffered message
/// </summary>
internal class BufferedMessageStoreModel
{
    public BufferedMessageStoreModel(string messageId, string bufferKey)
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