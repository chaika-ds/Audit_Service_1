namespace AuditService.Common.Enums;

/// <summary>
///     Sort type for losses log
/// </summary>
public enum LossesLogSortType
{
    /// <summary>
    ///     The amount of the last deposit
    ///     of the player's account on which the bet was made
    /// </summary>
    LastDeposit = 0,

    /// <summary>
    ///     Date and time of the event
    /// </summary>
    CreatedTime = 1
}