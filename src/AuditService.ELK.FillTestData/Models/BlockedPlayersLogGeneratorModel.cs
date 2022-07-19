using Newtonsoft.Json;

namespace AuditService.ELK.FillTestData.Models;

internal class BlockedPlayersLogGeneratorModel : BaseModel
{
    /// <summary>
    /// Fillers of configuration model
    /// </summary>
    [JsonProperty("Fillers")]
    public BlockedPlayersLogConfigurationModel[] Fillers { get; set; } = null!;
}