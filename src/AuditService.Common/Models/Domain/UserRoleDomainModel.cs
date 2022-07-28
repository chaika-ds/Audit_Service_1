using System.ComponentModel.DataAnnotations;

namespace AuditService.Common.Models.Domain;

/// <summary>
///     User role
/// </summary>
public class UserRoleDomainModel
{
    public UserRoleDomainModel(string code, string name)
    {
        Code = code;
        Name = name;
    }

    /// <summary>
    ///     Role identifier
    /// </summary>
    [Required]
    public string Code { get; set; }

    /// <summary>
    ///     Role name
    /// </summary>
    [Required]
    public string Name { get; set; }
}