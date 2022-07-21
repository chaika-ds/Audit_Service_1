using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.Login.Models;

/// <summary>
///     User login data
/// </summary>
public class LoginData
{
    public LoginData(string userId, string authToken, AboutMeInfo aboutMeInfo)
    {
        UserId = userId;
        AuthToken = authToken;
        AboutMeInfo = aboutMeInfo;
    }

    /// <summary>
    ///     User Id in the system
    /// </summary>
    [JsonProperty("userid")] 
    public string UserId { get; set; }

    /// <summary>
    ///     User token in the system
    /// </summary>
    [JsonProperty("authToken")] 
    public string AuthToken { get; set; }

    /// <summary>
    ///     Information about the authorized user
    /// </summary>
    [JsonProperty("me")] 
    public AboutMeInfo AboutMeInfo { get; set; }
}