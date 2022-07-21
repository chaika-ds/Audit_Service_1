using KIT.RocketChat.ApiClient.Methods.Login.Enums;
using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.Login.Models;

/// <summary>
///     Response model for executing the login method
/// </summary>
public class LoginResponse
{
    public LoginResponse(LoginStatus status, LoginData loginData)
    {
        Status = status;
        LoginData = loginData;
    }

    /// <summary>
    ///     Login method execution status
    /// </summary>
    [JsonProperty("status")]
    public LoginStatus Status { get; set; }

    /// <summary>
    ///     User login data
    /// </summary>
    [JsonProperty("data")]
    public LoginData LoginData { get; set; }
}