namespace AuditService.Common.Models.Dto.Filter.VisitLog;

/// <summary>
///     Filter model for player visit log
/// </summary>
public class PlayerVisitLogFilterDto : BaseVisitLogFilterDto
{
    /// <summary>
    ///     Player Id
    /// </summary>
    public Guid? PlayerId { get; set; }

    /// <summary>
    ///     Hall Id
    /// </summary>
    public Guid? HallId { get; set; }

    /// <summary>
    ///     Type/method of authorization
    /// </summary>
    public string? AuthorizationMethod { get; set; }
}