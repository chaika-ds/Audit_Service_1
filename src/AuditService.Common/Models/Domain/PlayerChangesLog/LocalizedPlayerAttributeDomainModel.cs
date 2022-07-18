using System.ComponentModel.DataAnnotations;

namespace AuditService.Common.Models.Domain.PlayerChangesLog;

/// <summary>
///     Localized attribute player, reflects changed fields
/// </summary>
public class LocalizedPlayerAttributeDomainModel : BasePlayerAttributeDomainModel
{
    public LocalizedPlayerAttributeDomainModel()
    {
        Value = string.Empty;
        Type = string.Empty;
        Label = string.Empty;
    }

    /// <summary>
    ///     localized attribute label
    /// </summary>
    [Required]
    public string Label { get; set; }
}