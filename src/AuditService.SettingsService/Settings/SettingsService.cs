using AuditService.SettingsService.Settings.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AuditService.SettingsService.Settings;

/// <summary>
///     Settings for settings service
/// </summary>
internal class SettingsService : ISettingsService
{
    public SettingsService(IConfiguration configuration)
    {
        Url = configuration["SettingsService:Url"];
    }

    /// <summary>
    ///     API URL for settings service
    /// </summary>
    public string Url { get; set; }
}