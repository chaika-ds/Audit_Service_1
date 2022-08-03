using AuditService.Localization.Localizer.Models;

namespace AuditService.Localization.Localizer.Storage;

/// <summary>
///     Storage for localization resources
/// </summary>
public interface ILocalizationStorage
{
    /// <summary>
    ///     Get localization resources from storage
    /// </summary>
    /// <param name="resourceParameters">Localization resource parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Localization resources</returns>
    Task<IDictionary<string, string>> GetResources(LocalizationResourceParameters resourceParameters, CancellationToken cancellationToken);

    /// <summary>
    ///     Set localization resources to storage
    /// </summary>
    /// <param name="localizationResources">Localization resources for store in storage</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task execution result</returns>
    Task SetResources(LocalizationResources localizationResources, CancellationToken cancellationToken);

    /// <summary>
    ///     Clear localization resources from storage
    /// </summary>
    /// <param name="resourceParameters">Localization resource parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task execution result</returns>
    Task ClearResources(LocalizationResourceParameters resourceParameters, CancellationToken cancellationToken);
}