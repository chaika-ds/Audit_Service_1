using System.ComponentModel.DataAnnotations;

namespace AuditService.Common.Models.Domain.AuditLog;

/// <summary>
///     User info
/// </summary>
public class AuditLogUserDomainModel
{
    public AuditLogUserDomainModel()
    {
        Ip = string.Empty;
        Login = string.Empty;
        UserAgent = string.Empty;
    }

    /// <summary>
    ///     User ID
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    ///     IP-adress
    /// </summary>
    [Required]
    public string Ip { get; set; }

    /// <summary>
    ///     Login/Email
    /// </summary>
    [Required]
    public string Login { get; set; }

    /// <summary>
    ///     Data about internet browser
    /// </summary>
    [Required]
    public string UserAgent { get; set; }
}