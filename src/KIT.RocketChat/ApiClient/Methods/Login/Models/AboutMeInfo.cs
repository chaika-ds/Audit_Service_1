using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.Login.Models;

/// <summary>
///     Information about the authorized user
/// </summary>
public class AboutMeInfo
{
    public AboutMeInfo()
    {
        Id = string.Empty;
        Emails = new List<Email>();
        Status = string.Empty;
        Roles = new List<string>();
        Name = string.Empty;
        StatusConnection = string.Empty;
        Username = string.Empty;
    }

    /// <summary>
    ///     User Id
    /// </summary>
    [JsonProperty("_id")] 
    public string Id { get; set; }

    /// <summary>
    ///     User services data
    /// </summary>
    [JsonProperty("services")] 
    public Services? Services { get; set; }

    /// <summary>
    ///     User email addresses
    /// </summary>
    [JsonProperty("emails")] 
    public IEnumerable<Email> Emails { get; set; }

    /// <summary>
    ///     User status
    /// </summary>
    [JsonProperty("status")] 
    public string Status { get; set; }

    /// <summary>
    ///     User activity
    /// </summary>
    [JsonProperty("active")] 
    public bool IsActive { get; set; }

    /// <summary>
    ///     Date and time the user was updated
    /// </summary>
    [JsonProperty("_updatedAt")]
    public DateTime UpdatedDate { get; set; }

    /// <summary>
    ///     User roles
    /// </summary>
    [JsonProperty("roles")] 
    public IEnumerable<string> Roles { get; set; }

    /// <summary>
    ///     User name
    /// </summary>
    [JsonProperty("name")] 
    public string Name { get; set; }

    /// <summary>
    ///     User connection status
    /// </summary>
    [JsonProperty("statusConnection")] 
    public string StatusConnection { get; set; }

    /// <summary>
    ///     User time zone by UTC
    /// </summary>
    [JsonProperty("utcOffset")] 
    public int UtcOffset { get; set; }

    /// <summary>
    ///     User name(login)
    /// </summary>
    [JsonProperty("username")] 
    public string Username { get; set; }

    /// <summary>
    ///     User default status
    /// </summary>
    [JsonProperty("statusDefault")] 
    public string? StatusDefault { get; set; }

    /// <summary>
    ///     User settings
    /// </summary>
    [JsonProperty("settings")] 
    public Settings? Settings { get; set; }

    /// <summary>
    ///     User text status
    /// </summary>
    [JsonProperty("statusText")] 
    public string? StatusText { get; set; }

    /// <summary>
    ///     User avatar(URL)
    /// </summary>
    [JsonProperty("avatarUrl")] 
    public string? AvatarUrl { get; set; }
}