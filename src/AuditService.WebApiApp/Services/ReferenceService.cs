using AuditService.Data.Domain.Domain;
using AuditService.Data.Domain.Enums;
using AuditService.WebApiApp.AppSettings;
using AuditService.WebApiApp.Services.Interfaces;
using Newtonsoft.Json;
using Tolar.Redis;

namespace AuditService.WebApiApp.Services;

/// <summary>
///     Reference provider (services\categories)
/// </summary>
public class ReferenceService : IReferenceService
{
    private readonly IJsonData _jsonData;
    private readonly IRedisRepository _redis;

    public ReferenceService(IJsonData jsonData, IRedisRepository redis)
    {
        _jsonData = jsonData;
        _redis = redis;
    }

    /// <summary>
    ///     Get available services
    /// </summary>
    public async Task<IEnumerable<CategoryBaseDomainModel>> GetServicesAsync()
    {
        string key = "ReferenceService.GetServicesAsync";

        var servicesCache = await _redis.GetAsync<IEnumerable<CategoryBaseDomainModel>>(key);

        if (servicesCache != null) return servicesCache;

        var allCategories = await GetCategoriesAsync();

        var categoryBaseDomainModels = new List<CategoryBaseDomainModel>();

        categoryBaseDomainModels.AddRange(allCategories.SelectMany(x => x.Value));

        await _redis.SetAsync(key, categoryBaseDomainModels, TimeSpan.FromMinutes(10));

        return categoryBaseDomainModels;
    }

    /// <summary>
    ///     Get available categories by filter
    /// </summary>
    /// <param name="serviceId">Service ID</param>
    public async Task<IDictionary<ServiceId, CategoryDomainModel[]>> GetCategoriesAsync(ServiceId? serviceId = null)
    {
        string key = "ReferenceService.GetCategoriesAsync_" + serviceId;

        var categoriesCache = await _redis.GetAsync<IDictionary<ServiceId, CategoryDomainModel[]>>(key);

        if (categoriesCache != null) return categoriesCache;

        var startupPath = Directory.GetParent(Environment.CurrentDirectory);

        var path = startupPath + "/" + _jsonData.ServiceCategories;

        using var reader = new StreamReader(path);

        var json = await reader.ReadToEndAsync();

        var categories = JsonConvert.DeserializeObject<IDictionary<ServiceId, CategoryDomainModel[]>>(json);
        if (categories == null)
            throw new FileNotFoundException(
                $"File {_jsonData.ServiceCategories} not found or not include data of categories.");

        var value = !serviceId.HasValue
            ? categories
            : categories.Where(w => w.Key == serviceId.Value).ToDictionary(w => w.Key, w => w.Value);

        await _redis.SetAsync(key, value, TimeSpan.FromMinutes(10));

        return value;
    }
}