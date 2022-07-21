using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.BaseEntities;

/// <summary>
///     Message
/// </summary>
public class Message
{
    public Message(string id)
    {
        Id = id;
    }

    /// <summary>
    ///     Message Id
    /// </summary>
    [JsonProperty("_id")]
    public string Id { get; set; }
}