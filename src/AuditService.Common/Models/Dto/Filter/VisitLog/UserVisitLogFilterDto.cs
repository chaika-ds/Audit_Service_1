namespace AuditService.Common.Models.Dto.Filter.VisitLog;

/// <summary>
///     Filter model for user visit log
/// </summary>
public class UserVisitLogFilterDto : BaseVisitLogFilterDto
{
    /// <summary>
    ///     User Id
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    ///     User role 
    /// </summary>
    public string? UserRole { get; set; }
}