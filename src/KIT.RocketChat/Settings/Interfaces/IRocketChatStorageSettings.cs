namespace KIT.RocketChat.Settings.Interfaces;

/// <summary>
///     API RocketChat data storage settings
/// </summary>
public interface IRocketChatStorageSettings
{
    /// <summary>
    ///     Authentication data lifetime in hours
    /// </summary>
    int? AuthDataLifetimeInHours { get; set; }

    /// <summary>
    ///     Message buffering time in minutes
    /// </summary>
    int? MessageBufferingTimeInMinutes { get; set; }
}