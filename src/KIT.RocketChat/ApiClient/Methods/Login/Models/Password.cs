using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.Login.Models;

/// <summary>
///     Password
/// </summary>
public class Password
{
    public Password(string bcrypt)
    {
        Bcrypt = bcrypt;
    }

    /// <summary>
    ///     Bcrypt
    /// </summary>
    [JsonProperty("bcrypt")]
    public string Bcrypt { get; set; }
}