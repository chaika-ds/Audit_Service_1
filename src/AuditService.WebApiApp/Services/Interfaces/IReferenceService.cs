using AuditService.Data.Domain.Domain;
using AuditService.Data.Domain.Enums;

namespace AuditService.WebApiApp.Services.Interfaces;

/// <summary>
///     Reference provider (services\categories)
/// </summary>
public interface IReferenceService
{
    /// <summary>
    ///     Get available services
    /// </summary>
    Task<IEnumerable<ServiceId>> GetServicesAsync();

    /// <summary>
    ///     Get available categories by filter
    /// </summary>
    /// <param name="serviceId">Service ID</param>
    Task<IDictionary<ServiceId, CategoryDomainModel[]>> GetCategoriesAsync(ServiceId? serviceId = null);
}