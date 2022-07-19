using Microsoft.Extensions.Configuration;

namespace AuditService.Localization.Settings;

/// <summary>
///     Settings for resource localization source
/// </summary>
internal class LocalizationSourceSettings : ILocalizationSourceSettings
{
    public LocalizationSourceSettings(IConfiguration configuration) => ApplySettings(configuration);

    /// <summary>
    ///     Uri template for downloading localization resources
    /// </summary>
    public string? UriTemplate { get; set; }

    /// <summary>
    ///     Apply settings
    /// </summary>
    private void ApplySettings(IConfiguration configuration) => UriTemplate = configuration["Localization:Source:UriTemplate"];
}