using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.PostMessage.Models;

/// <summary>
///     Attachment field object
/// </summary>
public class AttachmentField
{
    public AttachmentField(string title, string value)
    {
        Title = title;
        Value = value;
    }

    /// <summary>
    ///     Title object
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; }

    /// <summary>
    ///     Value object
    /// </summary>
    [JsonProperty("value")]
    public string Value { get; set; }
}