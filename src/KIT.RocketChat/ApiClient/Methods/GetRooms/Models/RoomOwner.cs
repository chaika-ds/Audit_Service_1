using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.GetRooms.Models;

/// <summary>
///     Room owner
/// </summary>
public class RoomOwner
{
    public RoomOwner(string id, string username)
    {
        Id = id;
        Username = username;
    }

    /// <summary>
    ///     User Id
    /// </summary>
    [JsonProperty("_id")]
    public string Id { get; set; }

    /// <summary>
    ///     User name
    /// </summary>
    [JsonProperty("username")]
    public string Username { get; set; }
}