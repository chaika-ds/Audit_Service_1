using Newtonsoft.Json;

namespace AuditService.ELK.FillTestData.Models;

/// <summary>
///     Base Model of all generator models
/// </summary>
internal class BaseModel
{
    /// <summary>
    ///     Clean before switcher
    /// </summary>
    public bool CleanBefore { get; set; }
    
    /// <summary>
    /// Fillers of configuration model
    /// </summary>
    [JsonProperty("Fillers")]
    public ConfigurationModel[] Fillers { get; set; } = null!;
}