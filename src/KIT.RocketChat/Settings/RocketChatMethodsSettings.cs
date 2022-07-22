using KIT.RocketChat.Settings.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KIT.RocketChat.Settings;

/// <summary>
///     API RocketChat methods settings
/// </summary>
internal class RocketChatMethodsSettings : IRocketChatMethodsSettings
{
    public RocketChatMethodsSettings(IConfiguration configuration)
    {
        ApplySettings(configuration);
    }

    /// <summary>
    ///     Api method - login
    /// </summary>
    public string? LoginMethod { get; set; }

    /// <summary>
    ///     Api method - Get all rooms
    /// </summary>
    public string? GetRoomsMethod { get; set; }

    /// <summary>
    ///     Api method - Post message
    /// </summary>
    public string? PostMessageMethod { get; set; }

    /// <summary>
    ///     Apply settings
    /// </summary>
    private void ApplySettings(IConfiguration configuration)
    {
        LoginMethod = configuration["RocketChat:Methods:Login"];
        GetRoomsMethod = configuration["RocketChat:Methods:GetRooms"];
        PostMessageMethod = configuration["RocketChat:Methods:PostMessage"];
    }
}