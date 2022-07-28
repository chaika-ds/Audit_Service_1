using System.ComponentModel.DataAnnotations;

namespace AuditService.Common.Models.Domain.VisitLog;

/// <summary>
///     Player visit log
/// </summary>
public class PlayerVisitLogDomainModel : BaseVisitLogDomainModel
{
    /// <summary>
    ///     Hall Id 
    /// </summary>
    [Required]
    public Guid HallId { get; set; }

    /// <summary>
    ///     Player Id
    /// </summary>
    [Required]
    public Guid PlayerId { get; set; }
}