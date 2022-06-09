using AuditService.Data.Domain.Domain;
using AuditService.Data.Domain.Enums;
using AuditService.WebApiApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuditService.Common.Logger;

namespace AuditService.WebApiApp.Controllers;

/// <summary>
///     Allows you to get a list of available services and categories
/// </summary>
[ApiController]
[Route("reference")]
public class ReferenceController
{
    private readonly IReferenceService _referenceService;

    /// <summary>
    ///     Allows you to get a list of available services and categories
    /// </summary>
    public ReferenceController(IReferenceService referenceService)
    {
        _referenceService = referenceService;
    }

    /// <summary>
    ///     Allows you to get a list of available services
    /// </summary>
    [HttpGet]
    [Route("services")]
    [Produces("application/json", Type = typeof(IEnumerable<ServiceId>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[ServiceFilter(typeof(LoggingActionFilter))]
    public async Task<IEnumerable<ServiceId>> GetServicesAsync()
    {
        return await _referenceService.GetServicesAsync();
    }

    /// <summary>
    ///     Allows you to get a list of available categories
    /// </summary>
    [HttpGet]
    [Route("categories")]
    [Produces("application/json", Type = typeof(IDictionary<ServiceId, CategoryDomainModel[]>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[ServiceFilter(typeof(LoggingActionFilter))]
    public async Task<IDictionary<ServiceId, CategoryDomainModel[]>> GetCategoriesAsync()
    {
        return await _referenceService.GetCategoriesAsync();
    }

    /// <summary>
    ///     Allows you to get a list of available categories by serviceId
    /// </summary>
    /// <param name="serviceId">Selected service id</param>
    [HttpGet]
    [Route("categories/{serviceId}")]
    [Produces("application/json", Type = typeof(IDictionary<ServiceId, CategoryDomainModel[]>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[ServiceFilter(typeof(LoggingActionFilter))]
    public async Task<IDictionary<ServiceId, CategoryDomainModel[]>> GetCategoriesAsync(ServiceId serviceId)
    {
        return await _referenceService.GetCategoriesAsync(serviceId);
    }
}