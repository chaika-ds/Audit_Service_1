using System.ComponentModel.DataAnnotations;
using AuditService.Common.Models.Domain.LossesLog;

namespace AuditService.Common.Models.Dto;

/// <summary>
///     Response model for losses log
/// </summary>
public class LossesLogResponseDto : LossesLogBaseModel
{
    public LossesLogResponseDto()
    {
        NodeName = string.Empty;
    }

    /// <summary>
    ///     Node name
    /// </summary>
    public string? NodeName { get; set; }

    /// <summary>
    ///     Date and time when the player reached the minimum
    /// </summary>
    [Required]
    public DateTime CreatedTime { get; set; }
}