namespace KIT.RocketChat.Settings.Interfaces;

/// <summary>
///     API RocketChat settings
/// </summary>
public interface IRocketChatApiSettings
{
    /// <summary>
    ///     API user to authenticate
    /// </summary>
    string? User { get; set; }

    /// <summary>
    ///     API password to authenticate
    /// </summary>
    string? Password { get; set; }

    /// <summary>
    ///     Base url for api
    /// </summary>
    string? BaseApiUrl { get; set; }

    /// <summary>
    ///     Api version
    /// </summary>
    string? ApiVersion { get; set; }
}