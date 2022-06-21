using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Providers.Interfaces;
using AuditService.Setup.ConfigurationSettings;
using AuditService.Setup.Extensions;
using Newtonsoft.Json;

namespace AuditService.Providers.Implementations;

/// <summary>
///     Reference processor (services\categories)
/// </summary>
public class ReferenceProvider : IReferenceProvider
{
    private readonly IJsonDataSettings _jsonDataSettings;

    public ReferenceProvider(IJsonDataSettings jsonDataSettings)
    {
        _jsonDataSettings = jsonDataSettings;
    }

    /// <summary>
    ///     Get available services
    /// </summary>
    public async Task<IEnumerable<CategoryBaseDomainModel>> GetServicesAsync()
    {
        return (await GetCategoriesAsync()).SelectMany(x => x.Value.Cast<CategoryBaseDomainModel>()).ToList();
    }

    /// <summary>
    ///     Get available categories by filter
    /// </summary>
    /// <param name="serviceId">Service ID</param>
    public async Task<IDictionary<ServiceStructure, CategoryDomainModel[]>> GetCategoriesAsync(ServiceStructure? serviceId = null)
    {
        var path = Directory.GetParent(Environment.CurrentDirectory) + "/AuditService.Common/" + _jsonDataSettings?.ServiceCategories;
        
        using var reader = new StreamReader(path ?? throw new NullReferenceException( $"{nameof(_jsonDataSettings.ServiceCategories)} is null"));
        var json = await reader.ReadToEndAsync();

        var categories = JsonConvert.DeserializeObject<IDictionary<ServiceStructure, CategoryDomainModel[]>>(json);
        if (categories == null)
            throw new FileNotFoundException(
                $"File {_jsonDataSettings?.ServiceCategories} not found or not include data of categories.");

        return !serviceId.HasValue
            ? categories
            : categories.Where(w => w.Key == serviceId.Value).ToDictionary(w => w.Key, w => w.Value);
    }
}