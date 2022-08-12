using System.ComponentModel.DataAnnotations;

namespace AuditService.Common.Models.Dto.VisitLog;

/// <summary>
///     Response model for base visit log
/// </summary>
public abstract class BaseVisitLogResponseDto
{
    protected BaseVisitLogResponseDto()
    {
        Login = string.Empty;
        Ip = string.Empty;
        DeviceType = string.Empty;
        OperatingSystem = string.Empty;
        Browser = string.Empty;
    }

    /// <summary>
    ///     Node Id
    /// </summary>
    [Required]
    public Guid NodeId { get; set; }

    /// <summary>
    ///     Login
    /// </summary>
    [Required]
    public string Login { get; set; }

    /// <summary>
    ///     IP address
    /// </summary>
    [Required]
    public string Ip { get; set; }

    /// <summary>
    ///     Device type
    /// </summary>
    [Required]
    public string DeviceType { get; set; }

    /// <summary>
    ///     Operating system
    /// </summary>
    [Required]
    public string OperatingSystem { get; set; }

    /// <summary>
    ///     The name of the browser from which authorization was performed
    /// </summary>
    [Required]
    public string Browser { get; set; }

    /// <summary>
    ///     Date and time of visit(Start date)
    /// </summary>
    [Required]
    public DateTime VisitTime { get; set; }
}