using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AuditService.Common.Models.Domain.PlayerChangesLog;

/// <summary>
///     Base attribute player, reflects changed fields
/// </summary>
public abstract class BasePlayerAttributeDomainModel
{
    protected BasePlayerAttributeDomainModel()
    {
        Value = string.Empty;
        Type = string.Empty;
    }

    /// <summary>
    ///     Attribute value or localization code
    /// </summary>
    [Required]
    [JsonProperty("value")]
    public string Value { get; set; }

    /// <summary>
    ///     Attribute type
    /// </summary>
    [Required]
    [JsonProperty("type")]
    public string Type { get; set; }
}