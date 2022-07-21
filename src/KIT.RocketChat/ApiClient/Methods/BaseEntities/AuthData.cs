namespace KIT.RocketChat.ApiClient.Methods.BaseEntities;

/// <summary>
///     RocketChat authentication data
/// </summary>
public class AuthData
{
    public AuthData(string userId, string authToken)
    {
        UserId = userId;
        AuthToken = authToken;
    }

    /// <summary>
    ///     User Id
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    ///     Token for authentication
    /// </summary>
    public string AuthToken { get; set; }
}