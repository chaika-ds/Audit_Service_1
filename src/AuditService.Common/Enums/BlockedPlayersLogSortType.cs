namespace AuditService.Common.Enums;

/// <summary>
///     Sort type for blocked player log
/// </summary>
public enum BlockedPlayersLogSortType
{
    /// <summary>
    ///     Date and time the lock was set
    /// </summary>
    BlockingDate = 0,

    /// <summary>
    ///     Date and time of the previous block
    /// </summary>
    PreviousBlockingDate = 1,

    /// <summary>
    ///     Counter of the number of player bans
    /// </summary>
    BlocksCounter = 2,

    /// <summary>
    ///     Browser version
    /// </summary>
    Version = 3
}