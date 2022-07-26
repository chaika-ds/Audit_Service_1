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
    ///     Date and time of the blocked (ISO 8601 UTC standard)
    /// </summary>
    [Required]
    public DateTime Timestamp { get; set; }
}