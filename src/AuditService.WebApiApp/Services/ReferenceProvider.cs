using AuditService.WebApiApp.Services.Interfaces;

namespace AuditService.WebApiApp.Services;

/// <summary>
///     Reference provider (services\categories)
/// </summary>
internal class ReferenceProvider : IReferenceProvider
{
    /// <summary>
    ///     Get available services
    /// </summary>
    public Task<IEnumerable<object>> GetServicesAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Get available categories by filter
    /// </summary>
    /// <param name="serviceId">Service ID</param>
    public async Task<IEnumerable<object>> GetCategoriesAsync(Guid? serviceId = null)
    {
        // todo подложить реальные данные после работы Камрана
        await Task.Delay(1);
        if (serviceId.HasValue)
            return new List<object> { serviceId };

        return new List<object>();
    }
}