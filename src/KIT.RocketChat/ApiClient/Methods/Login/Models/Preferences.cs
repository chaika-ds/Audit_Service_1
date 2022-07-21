using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.Login.Models;

/// <summary>
///     User preferences
/// </summary>
public class Preferences
{
    /// <summary>
    ///     Is auto image load
    /// </summary>
    [JsonProperty("autoImageLoad")] 
    public bool IsAutoImageLoad { get; set; }

    /// <summary>
    ///     Has collapse media by default
    /// </summary>
    [JsonProperty("collapseMediaByDefault")]
    public bool HasCollapseMediaByDefault { get; set; }

    /// <summary>
    ///     Need convert ascii emoji
    /// </summary>
    [JsonProperty("convertAsciiEmoji")] 
    public bool NeedConvertAsciiEmoji { get; set; }

    /// <summary>
    ///     Desktop notifications type
    /// </summary>
    [JsonProperty("desktopNotifications")] 
    public string? DesktopNotifications { get; set; }

    /// <summary>
    ///     Email notification mode
    /// </summary>
    [JsonProperty("emailNotificationMode")]
    public string? EmailNotificationMode { get; set; }

    /// <summary>
    ///     Need hide flex tab
    /// </summary>
    [JsonProperty("hideFlexTab")] 
    public bool NeedHideFlexTab { get; set; }

    /// <summary>
    ///     Need hide roles
    /// </summary>
    [JsonProperty("hideRoles")] 
    public bool NeedHideRoles { get; set; }

    /// <summary>
    ///     Need hide usernames
    /// </summary>
    [JsonProperty("hideUsernames")] 
    public bool NeedHideUsernames { get; set; }

    /// <summary>
    ///     Idle time limit
    /// </summary>
    [JsonProperty("idleTimeLimit")]
    public int IdleTimeLimit { get; set; }
}