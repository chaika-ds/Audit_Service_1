using System.ComponentModel.DataAnnotations;

namespace AuditService.Common.Models.Domain.PlayerChangesLog;

/// <summary>
///     The object of the user who made the changes (event initiator)
/// </summary>
public class UserInitiatorDomainModel
{
    public UserInitiatorDomainModel()
    {
        Email = string.Empty;
        UserAgent = string.Empty;
    }

    /// <summary>
    ///     User ID
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    ///     User email
    /// </summary>
    [Required]
    public string Email { get; set; }

    /// <summary>
    ///     Data about internet browser
    /// </summary>
    [Required]
    public string UserAgent { get; set; }
}