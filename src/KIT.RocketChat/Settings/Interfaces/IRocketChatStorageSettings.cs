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
    ///     Buffered message lifetime in minutes
    /// </summary>
    int? BufferedMessageLifetimeInMinutes { get; set; }

    /// <summary>
    ///     Buffered message block time in minutes
    /// </summary>
    int? BufferedMessageBlockLifetimeInMinutes { get; set; }
}