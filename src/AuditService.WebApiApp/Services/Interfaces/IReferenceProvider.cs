namespace AuditService.WebApiApp.Services.Interfaces;

/// <summary>
///     Reference provider (services\categories)
/// </summary>
public interface IReferenceProvider
{
    /// <summary>
    ///     Get available services
    /// </summary>
    Task<IEnumerable<object>> GetServicesAsync();

    /// <summary>
    ///     Get available categories by filter
    /// </summary>
    /// <param name="serviceId">Service ID</param>
    Task<IEnumerable<object>> GetCategoriesAsync(Guid? serviceId = null);
}