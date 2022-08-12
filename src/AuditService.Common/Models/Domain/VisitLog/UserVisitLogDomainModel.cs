using System.ComponentModel.DataAnnotations;

namespace AuditService.Common.Models.Domain.VisitLog;

/// <summary>
///     User visit log
/// </summary>
public class UserVisitLogDomainModel : BaseVisitLogDomainModel
{
    public UserVisitLogDomainModel()
    {
        UserRoles = new List<UserRoleDomainModel>();
    }
    
    /// <summary>
    ///     User Id
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    ///     List of user roles
    /// </summary>
    [Required]
    public List<UserRoleDomainModel> UserRoles { get; set; }
}