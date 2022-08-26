using AuditService.Localization.Localizer.Models;
using AuditService.Localization.Localizer.Storage;

namespace AuditService.Tests.Fakes.Localization;

/// <summary>
///      RedisCacheStorage fake
/// </summary>
public class RedisCacheStorageFake: ILocalizationStorage
{
    /// <summary>
    ///     Get localization resources from storage
    /// </summary>
    /// <param name="resourceParameters">Localization resource parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Localization resources</returns>
    public Task<IDictionary<string, string>> GetResources(LocalizationResourceParameters resourceParameters, CancellationToken cancellationToken)
    { 
        
        IDictionary<string, string> fakeRedisDictionary =  new Dictionary<string, string>
       {
           {"BI", "BUSINESS INTELLIGENCE SERVICE"}
       };

       return Task.FromResult(fakeRedisDictionary);
    }

    /// <summary>
    ///     Set localization resources to storage
    /// </summary>
    /// <param name="localizationResources">Localization resources for store in storage</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task execution result</returns>
    public Task SetResources(LocalizationResources localizationResources, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Clear localization resources from storage
    /// </summary>
    /// <param name="resourceParameters">Localization resource parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task execution result</returns>
    public Task ClearResources(LocalizationResourceParameters resourceParameters, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}