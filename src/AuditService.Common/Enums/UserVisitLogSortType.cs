namespace AuditService.Common.Enums;

/// <summary>
///     Sort type for user visit log
/// </summary>
public enum UserVisitLogSortType
{
    /// <summary>
    ///     Date and time of visit
    /// </summary>
    VisitTime = 0,

    /// <summary>
    ///     IP
    /// </summary>
    Ip = 1,

    /// <summary>
    ///     Node Id
    /// </summary>
    NodeId = 2,

    /// <summary>
    ///     Device type
    /// </summary>
    DeviceType = 3,

    /// <summary>
    ///     Operating system
    /// </summary>
    OperatingSystem = 4,

    /// <summary>
    ///     The name of the browser from which authorization was performed
    /// </summary>
    Browser = 5,

    /// <summary>
    ///     Login
    /// </summary>
    Login = 6
}