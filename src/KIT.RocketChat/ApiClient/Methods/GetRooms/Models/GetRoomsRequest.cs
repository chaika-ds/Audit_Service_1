
namespace KIT.RocketChat.ApiClient.Methods.GetRooms.Models;

/// <summary>
///     Request model for the execution of the getting rooms method
/// </summary>
public class GetRoomsRequest
{
    /// <summary>
    ///     Rooms have been updated since
    /// </summary>
    public DateTime? UpdatedSince { get; set; }
}