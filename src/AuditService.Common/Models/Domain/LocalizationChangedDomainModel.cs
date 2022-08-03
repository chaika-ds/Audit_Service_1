using System.ComponentModel.DataAnnotations;

namespace AuditService.Common.Models.Domain;

/// <summary>
///     Localization changed model
/// </summary>
public class LocalizationChangedDomainModel
{
    public LocalizationChangedDomainModel()
    {
        Service = string.Empty;
        Action = string.Empty;
        Type = string.Empty;
        Text = new List<Dictionary<string, string>>();
    }

    /// <summary>
    ///     Service identifier
    /// </summary>
    [Required]
    public string Service { get; set; }

    /// <summary>
    ///     System type
    /// </summary>
    [Required]
    public string Action { get; set; }

    /// <summary>
    ///     change - if the value of the key has been changed
    ///     publish - if the new publication of the key
    /// </summary>
    [Required]
    public string Type { get; set; }

    /// <summary>
    ///     List of objects containing changes
    /// </summary>
    public List<Dictionary<string, string>> Text { get; set; }
}