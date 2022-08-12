namespace AuditService.SettingsService.Settings.Interfaces;

/// <summary>
///     Settings for settings service
/// </summary>
public interface ISettingsService
{
    /// <summary>
    ///     API URL for settings service
    /// </summary>
    string Url { get; set; }
}