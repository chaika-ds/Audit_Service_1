using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.Login.Models;

/// <summary>
///     User settings
/// </summary>
public class Settings
{
    /// <summary>
    ///     User preferences
    /// </summary>
    [JsonProperty("preferences")]
    public Preferences? Preferences { get; set; }
}