using AuditService.Data.Domain.Dto;
using AuditService.Data.Domain.Enums;

namespace AuditService.WebApiApp.Services.Interfaces;

/// <summary>
///     Reference provider (services\categories)
/// </summary>
public interface IReferenceProvider
{
    /// <summary>
    ///     Get available services
    /// </summary>
    Task<IEnumerable<ServiceIdentity>> GetServicesAsync();

    /// <summary>
    ///     Get available categories by filter
    /// </summary>
    /// <param name="serviceId">Service ID</param>
    Task<IDictionary<ServiceIdentity, CategoryDto[]>> GetCategoriesAsync(ServiceIdentity? serviceId = null);
}