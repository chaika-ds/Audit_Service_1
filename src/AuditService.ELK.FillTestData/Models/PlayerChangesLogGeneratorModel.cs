using Newtonsoft.Json;

namespace AuditService.ELK.FillTestData.Models;

internal class PlayerChangesLogGeneratorModel: BaseModel
{
    /// <summary>
    /// Fillers of configuration model
    /// </summary>
    [JsonProperty("Fillers")]
    public PlayerChangesLogConfigurationModel[] Fillers { get; set; } = null!;
}