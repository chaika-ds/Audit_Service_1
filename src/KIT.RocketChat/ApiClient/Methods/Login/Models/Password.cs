using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.Login.Models;

/// <summary>
///     Password
/// </summary>
public class Password
{
    /// <summary>
    ///     Bcrypt
    /// </summary>
    [JsonProperty("bcrypt")]
    public string Bcrypt { get; set; }
}