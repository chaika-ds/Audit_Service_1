using KIT.RocketChat.Settings.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KIT.RocketChat.Settings;

/// <summary>
///     API RocketChat data storage settings
/// </summary>
internal class RocketChatStorageSettings : IRocketChatStorageSettings
{
    public RocketChatStorageSettings(IConfiguration configuration) => ApplySettings(configuration);

    /// <summary>
    ///     Authentication data lifetime in hours
    /// </summary>
    public int? AuthDataLifetimeInHours { get; set; }

    /// <summary>
    ///     Buffered message lifetime in minutes
    /// </summary>
    public int? BufferedMessageLifetimeInMinutes { get; set; }

    /// <summary>
    ///     Buffered message block time in minutes
    /// </summary>
    public int? BufferedMessageBlockLifetimeInMinutes { get; set; }

    /// <summary>
    ///     Apply settings
    /// </summary>
    private void ApplySettings(IConfiguration configuration)
    {
        AuthDataLifetimeInHours = Convert.ToInt32(configuration["RocketChat:Storage:AuthDataLifetimeInHours"]);
        BufferedMessageLifetimeInMinutes = Convert.ToInt32(configuration["RocketChat:Storage:BufferedMessageLifetimeInMinutes"]);
        BufferedMessageBlockLifetimeInMinutes = int.Parse(configuration["RocketChat:Storage:BufferedMessageBlockLifetimeInMinutes"]);
    }
}