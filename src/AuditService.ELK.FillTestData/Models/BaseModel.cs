using Newtonsoft.Json;

namespace AuditService.ELK.FillTestData.Models;

/// <summary>
///     Base Model of all generator models
/// </summary>
internal class BaseModel<TConfig>
{
    /// <summary>
    ///     Clean before switcher
    /// </summary>
    public bool CleanBefore { get; set; }
    
    /// <summary>
    /// Fillers of configuration model
    /// </summary>
    [JsonProperty("Fillers")]
    public TConfig[] Fillers { get; set; } = null!;
}