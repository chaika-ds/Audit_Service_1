using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AuditService.Common.Models.Domain.PlayerChangesLog;

/// <summary>
///     Attribute player, reflects changed fields
/// </summary>
public class PlayerAttributeDomainModel : BasePlayerAttributeDomainModel
{
    public PlayerAttributeDomainModel()
    {
        Value = string.Empty;
        Type = string.Empty;
        IsTranslatable = false;
    }

    /// <summary>
    ///     Flag indicating the need for translation
    /// </summary>
    [Required]
    [JsonProperty("isTranslatable")]
    public bool IsTranslatable { get; set; }
}