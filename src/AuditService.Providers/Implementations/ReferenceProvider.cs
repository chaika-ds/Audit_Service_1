using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Common.Resources;
using AuditService.Providers.Interfaces;
using Newtonsoft.Json;

namespace AuditService.Providers.Implementations;

/// <summary>
///     Reference processor (services\categories)
/// </summary>
public class ReferenceProvider : IReferenceProvider
{
    /// <summary>
    ///     Get available services
    /// </summary>
    public Task<IEnumerable<EnumResponseDto>> GetServicesAsync()
    {
        var result = Enum.GetValues<ServiceStructure>().Select(value => new EnumResponseDto(value.ToString(), value.Description()));
        return Task.FromResult(result);
    }

    /// <summary>
    ///     Get available categories by filter
    /// </summary>
    /// <param name="serviceId">Service ID</param>
    public async Task<IDictionary<ServiceStructure, CategoryDomainModel[]>> GetCategoriesAsync(ServiceStructure? serviceId = null)
    {
        var categories = JsonConvert.DeserializeObject<IDictionary<ServiceStructure, CategoryDomainModel[]>>(System.Text.Encoding.Default.GetString(JsonResource.ServiceCategories));
        if (categories == null)
            throw new FileNotFoundException( $"Not include data of categories.");

        var value =!serviceId.HasValue
            ? categories
            : categories.Where(w => w.Key == serviceId.Value).ToDictionary(w => w.Key, w => w.Value);

        return await Task.FromResult(value);
    }
}