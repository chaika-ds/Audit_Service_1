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
    ///     Message buffering time in minutes
    /// </summary>
    public int? MessageBufferingTimeInMinutes { get; set; }

    /// <summary>
    ///     Apply settings
    /// </summary>
    private void ApplySettings(IConfiguration configuration)
    {
        AuthDataLifetimeInHours = Convert.ToInt32(configuration["RocketChat:Storage:AuthDataLifetimeInHours"]);
        MessageBufferingTimeInMinutes = Convert.ToInt32(configuration["RocketChat:Storage:MessageBufferingTimeInMinutes"]);
    }
}