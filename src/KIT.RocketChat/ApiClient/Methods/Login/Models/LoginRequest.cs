using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.Login.Models;

/// <summary>
///     Request model for the execution of the login method
/// </summary>
public class LoginRequest
{
    public LoginRequest(string user, string password)
    {
        User = user;
        Password = password;
    }

    /// <summary>
    ///     User login
    /// </summary>
    [JsonProperty("user")]
    public string User { get; set; }

    /// <summary>
    ///     User password
    /// </summary>
    [JsonProperty("password")]
    public string Password { get; set; }
}