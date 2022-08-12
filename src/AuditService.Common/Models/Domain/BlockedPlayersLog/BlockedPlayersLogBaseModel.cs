using System.ComponentModel.DataAnnotations;

namespace AuditService.Common.Models.Domain.BlockedPlayersLog;

/// <summary>
///     Blocked player log base model
/// </summary>
public abstract class BlockedPlayersLogBaseModel : BlockedPlayersLogBriefBaseModel
{
    protected BlockedPlayersLogBaseModel()
    {
        LastVisitIpAddress = string.Empty;
        Platform = string.Empty;
    }

    /// <summary>
    ///     The IP address from which the login attempt was made before being blocked
    /// </summary>
    [Required]
    public string LastVisitIpAddress { get; set; }

    /// <summary>
    ///     The operating system of the device with which the player logged in
    /// </summary>
    [Required]
    public string Platform { get; set; }
}