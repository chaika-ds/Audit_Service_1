using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.GetRooms.Models;

/// <summary>
///     Response model for the execution of the getting rooms method
/// </summary>
public class GetRoomsResponse
{
    public GetRoomsResponse(IEnumerable<Room> rooms, IEnumerable<object> removedRooms, bool isSuccess)
    {
        Rooms = rooms;
        RemovedRooms = removedRooms;
        IsSuccess = isSuccess;
    }

    /// <summary>
    ///     User room
    /// </summary>
    [JsonProperty("update")]
    public IEnumerable<Room> Rooms { get; set; }

    /// <summary>
    ///     Removed rooms
    /// </summary>
    [JsonProperty("remove")]
    public IEnumerable<object> RemovedRooms { get; set; }

    /// <summary>
    ///     The success of the operation
    /// </summary>
    [JsonProperty("success")]
    public bool IsSuccess { get; set; }
}