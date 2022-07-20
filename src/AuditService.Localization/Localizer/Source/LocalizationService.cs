using AuditService.Localization.Localizer.Models;
using AuditService.Localization.Settings;
using KIT.NLog.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AuditService.Localization.Localizer.Source;

/// <summary>
///     Resource localization source
/// </summary>
internal class LocalizationService : ILocalizationSource
{
    private readonly ILocalizationSourceSettings _localizationSourceSettings;
    private readonly ILogger<LocalizationService> _logger;

    public LocalizationService(ILocalizationSourceSettings localizationSourceSettings, ILogger<LocalizationService> logger)
    {
        _localizationSourceSettings = localizationSourceSettings;
        _logger = logger;
    }

    /// <summary>
    ///     Load the localization resources
    /// </summary>
    /// <param name="resourceParameters">Localization resource parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Localization resources</returns>
    public async Task<IDictionary<string, string>> LoadResources(LocalizationResourceParameters resourceParameters, CancellationToken cancellationToken)
    {
        try
        {
            var url = string.Format(_localizationSourceSettings.UriTemplate!, resourceParameters.Service, resourceParameters.Language);
            using var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(url, cancellationToken);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json)!;
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Loading localization resources failed with an error", resourceParameters);
            return new Dictionary<string, string>();
        }
    }
}