using AuditService.Localization.Localizer.Models;
using AuditService.Localization.Localizer.Storage;

namespace AuditService.Tests.Fakes.Localization;

/// <summary>
///      RedisCacheStorage fake
/// </summary>
public class RedisCacheStorageFake: ILocalizationStorage
{
    public static LocalizationResourceParameters LocalizationResourceParameters = null!;
    public static  List<string> Languages = new();
    public static  int ClearResourcesCalled = 0;
    
    
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
    public async Task SetResources(LocalizationResources localizationResources, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    /// <summary>
    ///     Clear localization resources from storage
    /// </summary>
    /// <param name="resourceParameters">Localization resource parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task execution result</returns>
    public async Task ClearResources(LocalizationResourceParameters resourceParameters, CancellationToken cancellationToken)
    {
        Languages.Add(resourceParameters.Language);
        
        LocalizationResourceParameters = resourceParameters;

        ClearResourcesCalled++;
        
        await Task.CompletedTask;
    }
}