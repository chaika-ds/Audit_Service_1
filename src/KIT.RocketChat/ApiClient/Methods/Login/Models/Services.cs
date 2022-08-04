using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.Login.Models;

/// <summary>
///     User services data
/// </summary>
public class Services
{
    public Services(Password password)
    {
        Password = password;
    }

    /// <summary>
    ///     Password
    /// </summary>
    [JsonProperty("password")]
    public Password Password { get; set; }
}