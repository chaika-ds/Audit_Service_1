using System.ComponentModel.DataAnnotations;

namespace AuditService.Common.Models.Dto.VisitLog;

/// <summary>
///     Response model for player visit log
/// </summary>
public class PlayerVisitLogResponseDto : BaseVisitLogResponseDto
{
    public PlayerVisitLogResponseDto()
    {
        AuthorizationMethod = string.Empty;
    }

    /// <summary>
    ///     Player Id
    /// </summary>
    [Required]
    public Guid PlayerId { get; set; }

    /// <summary>
    ///     Type/method of authorization
    /// </summary>
    [Required]
    public string AuthorizationMethod { get; set; }
}