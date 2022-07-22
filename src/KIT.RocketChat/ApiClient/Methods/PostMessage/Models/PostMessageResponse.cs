using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.PostMessage.Models;

/// <summary>
///     Response model for the execution of the post message method
/// </summary>
public class PostMessageResponse
{
    public PostMessageResponse(string timeStamp, string? channel, Message message, bool isSuccess)
    {
        TimeStamp = timeStamp;
        Channel = channel;
        Message = message;
        IsSuccess = isSuccess;
    }

    /// <summary>
    ///     Message post time
    /// </summary>
    [JsonProperty("ts")]
    public string TimeStamp { get; set; }

    /// <summary>
    ///     Channel name
    /// </summary>
    [JsonProperty("channel")]
    public string? Channel { get; set; }

    /// <summary>
    ///     Message object
    /// </summary>
    [JsonProperty("message")]
    public Message Message { get; set; }

    /// <summary>
    ///     Sent message
    /// </summary>
    [JsonProperty("success")]
    public bool IsSuccess { get; set; }
}