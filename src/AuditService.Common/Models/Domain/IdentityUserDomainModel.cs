using System.ComponentModel.DataAnnotations;

namespace AuditService.Common.Models.Domain;

/// <summary>
///     User info
/// </summary>
public class IdentityUserDomainModel
{
    public IdentityUserDomainModel()
    {
        Ip = string.Empty;
        Email = string.Empty;
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
    public string Email { get; set; }

    /// <summary>
    ///     Data about internet browser
    /// </summary>
    [Required]
    public string UserAgent { get; set; }
}