using System.ComponentModel.DataAnnotations;
using AuditService.Common.Enums;

namespace AuditService.Common.Models.Domain.SsoUserChangesLog;

/// <summary>
///     SSO user changes log
/// </summary>
public class SsoUserChangesLogDomainModel
{
    public SsoUserChangesLogDomainModel()
    {
        UserRoles = new List<UserRoleDomainModel>();
        UserIp = string.Empty;
        UserLogin = string.Empty;
        UserAuthorization = new AuthorizationDataDomainModel();
        EventType = string.Empty;
    }

    /// <summary>
    ///     Node Id
    /// </summary>
    [Required]
    public Guid NodeId { get; set; }

    /// <summary>
    ///     Node type
    /// </summary>
    [Required]
    public NodeType NodeType { get; set; }

    /// <summary>
    ///     Project Id
    /// </summary>
    [Required]
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     User Id
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    ///     User login
    /// </summary>
    [Required]
    public string UserLogin { get; set; }

    /// <summary>
    ///     List of user roles
    /// </summary>
    public List<UserRoleDomainModel> UserRoles { get; set; }

    /// <summary>
    ///     User IP
    /// </summary>
    [Required]
    public string UserIp { get; set; }

    /// <summary>
    ///     An object containing user authorization data
    /// </summary>
    [Required]
    public AuthorizationDataDomainModel UserAuthorization { get; set; }

    /// <summary>
    ///     Login date and time
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    ///     Event type
    /// </summary>
    [Required]
    public string EventType { get; set; }
}