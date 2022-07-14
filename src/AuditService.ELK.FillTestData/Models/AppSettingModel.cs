using Newtonsoft.Json;

namespace AuditService.ELK.FillTestData.Models;

/// <summary>
///     Model of app settings
/// </summary>
internal class AppSettingModel
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