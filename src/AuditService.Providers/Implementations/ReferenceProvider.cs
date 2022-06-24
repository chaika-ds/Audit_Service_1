using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Common.Resources;
using AuditService.Providers.Interfaces;
using AuditService.Setup.Extensions;
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
        var enumResponseDtoList = Enum.GetNames(typeof(ServiceStructure)).Select(value => 
            new EnumResponseDto(value, ((ServiceStructure) Enum.Parse(typeof(ServiceStructure), value)).Description()));

        return Task.FromResult(enumResponseDtoList);
    }

    /// <summary>
    ///     Get available categories by filter
    /// </summary>
    /// <param name="serviceId">Service ID</param>
    public async Task<IDictionary<ServiceStructure, CategoryDomainModel[]>> GetCategoriesAsync(ServiceStructure? serviceId = null)
    {
        var categories = JsonConvert.DeserializeObject<IDictionary<ServiceStructure, CategoryDomainModel[]>>(JsonResource.Categories);
        if (categories == null)
            throw new FileNotFoundException( $"Not include data of categories.");

        var value =!serviceId.HasValue
            ? categories
            : categories.Where(w => w.Key == serviceId.Value).ToDictionary(w => w.Key, w => w.Value);

        return await Task.FromResult(value);
    }
}