using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Providers.Interfaces;
using AuditService.Utility.Logger.Filters;
using Microsoft.AspNetCore.Mvc;
using Tolar.Authenticate;

namespace AuditService.WebApi.Controllers;

/// <summary>
///     Allows you to get a list of available services and categories
/// </summary>
[ApiController]
[Route("reference")]
public class ReferenceController
{
    private readonly IReferenceProvider _referenceProcessor;

    /// <summary>
    ///     Allows you to get a list of available services and categories
    /// </summary>
    public ReferenceController(IReferenceProvider referenceProcessor)
    {
        _referenceProcessor = referenceProcessor;
    }

    /// <summary>
    ///     Allows you to get a list of available services
    /// </summary>
    [HttpGet]
    [Route("services")]
    [Authorize("Audit.Journal.GetAuditlog")]
    [Produces("application/json", Type = typeof(IEnumerable<ServiceStructure>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<IEnumerable<EnumResponseDto>> GetServicesAsync()
    {
        return await _referenceProcessor.GetServicesAsync();
    }

    /// <summary>
    ///     Allows you to get a list of available categories
    /// </summary>
    [HttpGet]
    [Route("categories")]
    [Authorize("Audit.Journal.GetAuditlog")]
    [Produces("application/json", Type = typeof(IDictionary<ServiceStructure, CategoryDomainModel[]>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<IDictionary<ServiceStructure, CategoryDomainModel[]>> GetCategoriesAsync()
    {
        return await _referenceProcessor.GetCategoriesAsync();
    }

    /// <summary>
    ///     Allows you to get a list of available categories by serviceId
    /// </summary>
    /// <param name="service">Selected service id</param>
    [HttpGet]
    [Route("categories/{service}")]
    [Authorize("Audit.Journal.GetAuditlog")]
    [Produces("application/json", Type = typeof(IDictionary<ServiceStructure, CategoryDomainModel[]>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<IDictionary<ServiceStructure, CategoryDomainModel[]>> GetCategoriesAsync(ServiceStructure service)
    {
        return await _referenceProcessor.GetCategoriesAsync(service);
    }
}