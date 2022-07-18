namespace AuditService.Common.Enums;

/// <summary>
///     Enumeration of event initiator
/// </summary>
public enum EventInitiator
{
    /// <summary>
    ///     Admin user
    /// </summary>
    User = 0,

    /// <summary>
    ///     Player
    /// </summary>
    Player = 1,

    /// <summary>
    ///     System
    /// </summary>
    System = 3
}