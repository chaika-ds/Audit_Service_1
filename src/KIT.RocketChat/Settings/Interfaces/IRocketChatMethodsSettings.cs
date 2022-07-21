namespace KIT.RocketChat.Settings.Interfaces;

/// <summary>
///     API RocketChat methods settings
/// </summary>
public interface IRocketChatMethodsSettings
{
    /// <summary>
    ///     Api method - login
    /// </summary>
    string? LoginMethod { get; set; }

    /// <summary>
    ///     Api method - Get all rooms
    /// </summary>
    string? GetRoomsMethod { get; set; }

    /// <summary>
    ///     Api method - Post message
    /// </summary>
    string? PostMessageMethod { get; set; }
}