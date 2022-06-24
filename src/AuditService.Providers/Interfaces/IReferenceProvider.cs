using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;

namespace AuditService.Providers.Interfaces;

/// <summary>
///     Reference provider (services\categories)
/// </summary>
public interface IReferenceProvider
{
    /// <summary>
    ///     Get available services
    /// </summary>
    Task<IEnumerable<EnumResponseDto>> GetServicesAsync();

    /// <summary>
    ///     Get available categories by filter
    /// </summary>
    /// <param name="serviceId">Service ID</param>
    Task<IDictionary<ServiceStructure, CategoryDomainModel[]>> GetCategoriesAsync(ServiceStructure? serviceId = null);
}