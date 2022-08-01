using AuditService.Common.Enums;

namespace AuditService.Common.Models.Domain.VisitLog;

/// <summary>
///     Visit log
/// </summary>
public class VisitLogDomainModel : BaseVisitLogDomainModel
{
    /// <summary>
    ///     Hall Id 
    /// </summary>
    public Guid? HallId { get; set; }

    /// <summary>
    ///     Player Id
    /// </summary>
    public Guid? PlayerId { get; set; }

    /// <summary>
    ///     Node Id
    /// </summary>
    public Guid? NodeId { get; set; }

    /// <summary>
    ///     Node type
    /// </summary>
    public NodeType? NodeType { get; set; }

    /// <summary>
    ///     User Id
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    ///     List of user roles
    /// </summary>
    public List<UserRoleDomainModel>? UserRoles { get; set; }
}