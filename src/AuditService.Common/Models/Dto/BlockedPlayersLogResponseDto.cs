using System.ComponentModel.DataAnnotations;
using AuditService.Common.Models.Domain.BlockedPlayersLog;

namespace AuditService.Common.Models.Dto;

/// <summary>
///     Blocked player log response model
/// </summary>
public class BlockedPlayersLogResponseDto : BlockedPlayersLogBriefBaseModel
{
    public BlockedPlayersLogResponseDto()
    {
        PlayerLogin = string.Empty;
        PlayerIp = string.Empty;
        OperatingSystem = string.Empty;
        Browser = string.Empty;
        Language = string.Empty;
    }

    /// <summary>
    ///     The IP address from which the login attempt was made before being blocked
    /// </summary>
    [Required]
    public string PlayerIp { get; set; }

    /// <summary>
    ///     The operating system of the device with which the player logged in
    /// </summary>
    [Required]
    public string OperatingSystem { get; set; }

    /// <summary>
    ///     Date and time of the blocked (ISO 8601 UTC standard)
    /// </summary>
    [Required]
    public DateTime Timestamp { get; set; }

    /// <summary>
    ///     Node name
    /// </summary>
    public string? NodeName { get; set; }
}