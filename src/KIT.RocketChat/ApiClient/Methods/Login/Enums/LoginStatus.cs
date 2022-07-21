using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.Login.Enums;

/// <summary>
///     Login method execution status
/// </summary>
public enum LoginStatus
{
    /// <summary>
    ///     Successful login
    /// </summary>
    [JsonProperty("success")] 
    Success = 0,

    /// <summary>
    ///     Login with error
    /// </summary>
    [JsonProperty("error")] 
    Error = 1
}