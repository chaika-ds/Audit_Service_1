using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.PostMessage.Models;

/// <summary>
///     Request model for the execution of the post message method
/// </summary>
public class PostMessageRequest
{
    public PostMessageRequest()
    {
        RoomId = string.Empty;
    }

    /// <summary>
    ///     Room Id
    ///     The room id of where the message is to be sent.
    ///     The channel name with the prefix in front of it.
    ///     # refers to channel, however @ refers to username
    /// </summary>
    [JsonProperty("roomId")]
    public string RoomId { get; set; }

    /// <summary>
    ///     Channel name
    /// </summary>
    [JsonProperty("channel")]
    public string? Channel { get; set; }

    /// <summary>
    ///     The text of the message to send, is optional because of attachments.
    /// </summary>
    [JsonProperty("text")]
    public string? Text { get; set; }

    /// <summary>
    ///     This will cause the message's name to appear as the given alias, but your username will still display.
    /// </summary>
    [JsonProperty("alias")]
    public string? Alias { get; set; }

    /// <summary>
    ///     If provided, this will make the avatar on this message be an emoji.
    /// </summary>
    [JsonProperty("emoji")]
    public string? Emoji { get; set; }

    /// <summary>
    ///     If provided, this will make the avatar use the provided image url.
    /// </summary>
    [JsonProperty("avatar")]
    public string? AvatarUrl { get; set; }

    /// <summary>
    ///     Message Id, if needed to be sent in a thread
    /// </summary>
    [JsonProperty("tmid")]
    public string? ThreadMessageId { get; set; }

    /// <summary>
    ///     Attachment files
    /// </summary>
    [JsonProperty("attachments")]
    public IEnumerable<Attachment>? Attachments { get; set; }

    /// <summary>
    /// Should serialize thread message Id
    /// </summary>
    /// <returns></returns>
    public bool ShouldSerializeThreadMessageId() => !string.IsNullOrEmpty(ThreadMessageId);
}