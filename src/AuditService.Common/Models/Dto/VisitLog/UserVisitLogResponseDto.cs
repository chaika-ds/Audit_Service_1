using System.ComponentModel.DataAnnotations;
using AuditService.Common.Models.Domain;

namespace AuditService.Common.Models.Dto.VisitLog;

/// <summary>
///     Response model for user visit log
/// </summary>
public class UserVisitLogResponseDto : BaseVisitLogResponseDto
{
    public UserVisitLogResponseDto()
    {
        UserRoles = new List<UserRoleDomainModel>();
    }

    /// <summary>
    ///     User Id
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    ///     User roles
    /// </summary>
    public List<UserRoleDomainModel> UserRoles { get; set; }

    /// <summary>
    ///     Node Id
    /// </summary>
    [Required]
    public Guid NodeId { get; set; }
}