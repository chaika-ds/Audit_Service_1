using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Providers.Interfaces;
using AuditService.Setup.ConfigurationSettings;
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
        var categoryBaseDomainModels = new List<CategoryBaseDomainModel>();

        var categories = (await GetCategoriesAsync()).SelectMany(x => x.Value).ToList();

        categoryBaseDomainModels.AddRange(categories);

        return categoryBaseDomainModels;
    }

    /// <summary>
    ///     Get available categories by filter
    /// </summary>
    /// <param name="serviceId">Service ID</param>
    public async Task<IDictionary<ServiceId, CategoryDomainModel[]>> GetCategoriesAsync(ServiceId? serviceId = null)
    {
        using var reader = new StreamReader(_jsonDataSettings.ServiceCategories ?? throw new NullReferenceException($"{nameof(_jsonDataSettings.ServiceCategories)} is null"));
        var json = await reader.ReadToEndAsync();

        var categories = JsonConvert.DeserializeObject<IDictionary<ServiceId, CategoryDomainModel[]>>(json);
        if (categories == null)
            throw new FileNotFoundException(
                $"File {_jsonDataSettings.ServiceCategories} not found or not include data of categories.");

        return !serviceId.HasValue
            ? categories
            : categories.Where(w => w.Key == serviceId.Value).ToDictionary(w => w.Key, w => w.Value);
    }
}