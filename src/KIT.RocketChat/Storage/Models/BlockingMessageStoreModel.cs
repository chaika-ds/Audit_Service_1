namespace KIT.RocketChat.Storage.Models;

/// <summary>
///     Model store for blocking message
/// </summary>
internal class BlockingMessageStoreModel
{
    public BlockingMessageStoreModel(string messageId)
    {
        MessageId = messageId;
    }

    /// <summary>
    ///     Blocked message Id
    /// </summary>
    public string MessageId { get; set; }
}