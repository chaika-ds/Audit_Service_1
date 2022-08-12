
namespace AuditService.Common.Models.Domain.VisitLog;

/// <summary>
///     Visit log
/// </summary>
public class VisitLogDomainModel : BaseVisitLogDomainModel
{
    /// <summary>
    ///     Player Id
    /// </summary>
    public Guid? PlayerId { get; set; }

    /// <summary>
    ///     User Id
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    ///     List of user roles
    /// </summary>
    public List<UserRoleDomainModel>? UserRoles { get; set; }
}