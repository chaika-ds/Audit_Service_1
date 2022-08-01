using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AuditService.Common.Models.Domain;
using Tolar.Export.Attributes;

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
    [ExportIgnore]
    public List<UserRoleDomainModel> UserRoles { get; set; }

    /// <summary>
    ///     User roles in string format
    /// </summary>
    [ExportName("UserRoles")]
    [JsonIgnore]
    public List<string> UserRolesStrings  => UserRoles.Select(userRole => $"{userRole.Code}={userRole.Name}").ToList();

    /// <summary>
    ///     Node Id
    /// </summary>
    [Required]
    public Guid NodeId { get; set; }
}