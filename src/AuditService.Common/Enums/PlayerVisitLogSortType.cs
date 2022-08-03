namespace AuditService.Common.Enums;

/// <summary>
///     Sort type for player visit log
/// </summary>
public enum PlayerVisitLogSortType
{
    /// <summary>
    ///     Date and time of visit
    /// </summary>
    VisitTime = 0,

    /// <summary>
    ///     Player Id
    /// </summary>
    PlayerId = 1,

    /// <summary>
    ///     IP
    /// </summary>
    Ip = 2,

    /// <summary>
    ///     Hall Id
    /// </summary>
    HallId = 3,

    /// <summary>
    ///     Device type
    /// </summary>
    DeviceType = 4,

    /// <summary>
    ///     Operating system
    /// </summary>
    OperatingSystem = 5,

    /// <summary>
    ///     The name of the browser from which authorization was performed
    /// </summary>
    Browser = 6,

    /// <summary>
    ///     Login
    /// </summary>
    Login = 7,

    /// <summary>
    ///     Type/method of authorization
    /// </summary>
    AuthorizationMethod = 8
}