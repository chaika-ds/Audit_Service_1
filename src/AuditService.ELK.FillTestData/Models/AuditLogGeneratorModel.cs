using Newtonsoft.Json;

namespace AuditService.ELK.FillTestData.Models;

/// <summary>
///     Model of app settings
/// </summary>
internal class AuditLogGeneratorModel : BaseModel
{
    /// <summary>
    /// Fillers of configuration model
    /// </summary>
    [JsonProperty("Fillers")]
    public AuditLogConfigurationModel[] Fillers { get; set; } = null!;
}