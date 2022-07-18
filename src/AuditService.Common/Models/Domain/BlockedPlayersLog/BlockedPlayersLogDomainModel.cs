using System.ComponentModel.DataAnnotations;
namespace AuditService.Common.Models.Domain.BlockedPlayersLog;

/// <summary>
///     Log of blocked players
/// </summary>
public class BlockedPlayersLogDomainModel : BlockedPlayersLogBaseModel
{
    public BlockedPlayersLogDomainModel()
    {
        HallName = string.Empty;
        ProjectName = string.Empty;
        PlayerLogin = string.Empty;
        LastVisitIpAddress = string.Empty;
        Platform = string.Empty;
        Browser = string.Empty;
        BrowserVersion = string.Empty;
        Language = string.Empty;
    }

    /// <summary>
    ///     Hall name
    /// </summary>
    [Required]
    public string HallName { get; set; }

    /// <summary>
    ///     ID of the project where the hall is located
    /// </summary>
    [Required]
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Project name
    /// </summary>
    [Required]
    public string ProjectName { get; set; }

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