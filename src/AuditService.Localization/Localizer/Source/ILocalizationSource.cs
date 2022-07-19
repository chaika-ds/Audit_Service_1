using AuditService.Localization.Localizer.Models;

namespace AuditService.Localization.Localizer.Source;

/// <summary>
///     Resource localization source
/// </summary>
public interface ILocalizationSource
{
    /// <summary>
    ///     Load the localization resources
    /// </summary>
    /// <param name="resourceParameters">Localization resource parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Localization resources</returns>
    Task<IDictionary<string, string>> LoadResources(LocalizationResourceParameters resourceParameters, CancellationToken cancellationToken);
}