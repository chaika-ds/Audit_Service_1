namespace AuditService.Common.Models.Dto.Filter.VisitLog;

/// <summary>
///     Base filter model for visit log
/// </summary>
public abstract class BaseVisitLogFilterDto
{
    /// <summary>
    ///     Login
    /// </summary>
    public string? Login { get; set; }

    /// <summary>
    ///     Ip
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    ///     Device type
    /// </summary>
    public string? DeviceType { get; set; }

    /// <summary>
    ///     Operating system
    /// </summary>
    public string? OperatingSystem { get; set; }

    /// <summary>
    ///     The name of the browser from which authorization was performed
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    ///     Date and time of visit(Start date)
    /// </summary>
    public DateTime? VisitTimeFrom { get; set; }

    /// <summary>
    ///     Date and time of visit(End date)
    /// </summary>
    public DateTime? VisitTimeTo { get; set; }
}