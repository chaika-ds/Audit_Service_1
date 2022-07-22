using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.GetRooms.Models;

/// <summary>
///     User room
/// </summary>
public class Room
{
    public Room()
    {
        Id = string.Empty;
        Type = string.Empty;
    }

    /// <summary>
    ///     Room Id
    /// </summary>
    [JsonProperty("_id")]
    public string Id { get; set; }

    /// <summary>
    ///     Room name
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    ///     Additional room name
    /// </summary>
    [JsonProperty("fname")]
    public string? Fname { get; set; }

    /// <summary>
    ///     Room custom fields
    /// </summary>
    [JsonProperty("customFields")]
    public object? CustomFields { get; set; }

    /// <summary>
    ///     Room description
    /// </summary>
    [JsonProperty("description")]
    public string? Description { get; set; }

    /// <summary>
    ///     Room type
    /// </summary>
    [JsonProperty("t")]
    public string Type { get; set; }

    /// <summary>
    ///     Count of messages
    /// </summary>
    [JsonProperty("msgs")]
    public int MessagesCount { get; set; }

    /// <summary>
    ///     Count of users
    /// </summary>
    [JsonProperty("usersCount")]
    public int UsersCount { get; set; }

    /// <summary>
    ///     Room update time
    /// </summary>
    [JsonProperty("_updatedAt")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    ///     Room owner
    /// </summary>
    [JsonProperty("u")]
    public RoomOwner? RoomOwner { get; set; }

    /// <summary>
    ///     Last message in room
    /// </summary>
    [JsonProperty("lastMessage")]
    public Message? LastMessage { get; set; }

    /// <summary>
    ///     Room is default
    /// </summary>
    [JsonProperty("default")]
    public bool IsDefault { get; set; }

    /// <summary>
    ///     User names of room
    /// </summary>
    [JsonProperty("usernames")]
    public IEnumerable<string>? Usernames { get; set; }

    /// <summary>
    ///     User ids of room
    /// </summary>
    [JsonProperty("uids")]
    public IEnumerable<string>? UserIds { get; set; }
}