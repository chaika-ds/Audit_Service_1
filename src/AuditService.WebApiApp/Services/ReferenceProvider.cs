using AuditService.Data.Domain.Dto;
using AuditService.Data.Domain.Enums;
using AuditService.WebApiApp.Services.Interfaces;
using Newtonsoft.Json;

namespace AuditService.WebApiApp.Services;

/// <summary>
///     Reference provider (services\categories)
/// </summary>
internal class ReferenceProvider : IReferenceProvider
{
    private readonly IJsonData _jsonData;

    public ReferenceProvider(IJsonData jsonData)
    {
        _jsonData = jsonData;
    }

    /// <summary>
    ///     Get available services
    /// </summary>
    public Task<IEnumerable<ServiceIdentity>> GetServicesAsync()
    {
        return Task.FromResult(Enum.GetValues(typeof(ServiceIdentity)).Cast<ServiceIdentity>());
    }

    /// <summary>
    ///     Get available categories by filter
    /// </summary>
    /// <param name="serviceId">Service ID</param>
    public async Task<IDictionary<ServiceIdentity, CategoryDto[]>> GetCategoriesAsync(ServiceIdentity? serviceId = null)
    {
        using var reader = new StreamReader(_jsonData.ServiceCategories);
        var json = await reader.ReadToEndAsync();

        var categories = JsonConvert.DeserializeObject<IDictionary<ServiceIdentity, CategoryDto[]>>(json);
        if (categories == null)
            throw new FileNotFoundException($"File {_jsonData.ServiceCategories} not found or not include data of categories.");

        return !serviceId.HasValue
            ? categories
            : categories.Where(w => w.Key == serviceId.Value).ToDictionary(w => w.Key, w => w.Value);
    }
}