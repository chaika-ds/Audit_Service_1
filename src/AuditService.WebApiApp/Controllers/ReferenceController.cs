using AuditService.Common.Logger;
using AuditService.Data.Domain.Dto;
using AuditService.Data.Domain.Enums;
using AuditService.WebApiApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.WebApiApp.Controllers;

/// <summary>
///     Allows you to get a list of available services and categories
/// </summary>
[ApiController]
[Route("reference")]
public class ReferenceController
{
    private readonly IReferenceProvider _referenceProvider;

    /// <summary>
    ///     Allows you to get a list of available services and categories
    /// </summary>
    public ReferenceController(IReferenceProvider referenceProvider)
    {
        _referenceProvider = referenceProvider;
    }

    /// <summary>
    ///     Allows you to get a list of available services
    /// </summary>
    [ServiceFilter(typeof(LoggingActionFilter))]
    [HttpGet]
    [Route("services")]
    public async Task<IEnumerable<ServiceIdentity>> GetServicesAsync()
    {
        return await _referenceProvider.GetServicesAsync();
    }

    /// <summary>
    ///     Allows you to get a list of available categories
    /// </summary>
    [ServiceFilter(typeof(LoggingActionFilter))]
    [HttpGet]
    [Route("categories")]
    public async Task<IDictionary<ServiceIdentity, CategoryDto[]>> GetCategoriesAsync()
    {
        return await _referenceProvider.GetCategoriesAsync();
    }

    /// <summary>
    ///     Allows you to get a list of available categories by serviceId
    /// </summary>
    /// <param name="serviceId">Selected service id</param>
    [ServiceFilter(typeof(LoggingActionFilter))]
    [HttpGet]
    [Route("categories/{serviceId}")]
    public async Task<IDictionary<ServiceIdentity, CategoryDto[]>> GetCategoriesAsync(ServiceIdentity serviceId)
    {
        return await _referenceProvider.GetCategoriesAsync(serviceId);
    }
}