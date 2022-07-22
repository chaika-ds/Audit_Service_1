namespace KIT.RocketChat.Commands.PostBufferedTextMessage.Models
{
    /// <summary>
    /// Request model for post a buffered message in a thread
    /// </summary>
    /// <param name="RoomName">Room name</param>
    /// <param name="Message">Message</param>
    /// <param name="BufferKey">Buffer key</param>
    public record PostBufferedMessageRequest(string RoomName, string Message, string BufferKey);
}
