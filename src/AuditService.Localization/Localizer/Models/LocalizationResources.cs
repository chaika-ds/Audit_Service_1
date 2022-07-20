namespace AuditService.Localization.Localizer.Models;

/// <summary>
///     Localization resources
/// </summary>
/// <param name="Resources">Resources</param>
/// <param name="ResourceParameters">Localization resource parameters</param>
public record LocalizationResources(IDictionary<string, string> Resources, LocalizationResourceParameters ResourceParameters);

/// <summary>
///     Localization resource parameters
/// </summary>
/// <param name="Service">Service key</param>
/// <param name="Language">Translation language</param>
public record LocalizationResourceParameters(string Service, string Language);
