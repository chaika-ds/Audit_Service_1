﻿using AuditService.Utility.Logger;
using AuditService.Data.Domain.Dto;
using AuditService.Data.Domain.Domain;
using AuditService.Data.Domain.Enums;
using AuditService.WebApiApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tolar.Authenticate;

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
    ///  Allows you to get a list of available services and categories
    /// </summary>
    public ReferenceController(IReferenceService referenceService)
    {
        _referenceService = referenceService;
    }

    /// <summary>
    /// Allows you to get a list of available services
    /// </summary>
    [Authorize("AuditService.Reference.viewServices")]
    [HttpGet]
    [Route("services")]
    [Produces("application/json", Type = typeof(IEnumerable<ServiceId>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<IEnumerable<CategoryBaseDomainModel>> GetServicesAsync()
    { 
        return await _referenceService.GetServicesAsync();
    }

    /// <summary>
    /// Allows you to get a list of available categories
    /// </summary>
    [Authorize("AuditService.Reference.viewCategories")]
    [HttpGet]
    [Route("categories")]
    [Produces("application/json", Type = typeof(IDictionary<ServiceId, CategoryDomainModel[]>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<IDictionary<ServiceId, CategoryDomainModel[]>> GetCategoriesAsync()
    {
        return await _referenceService.GetCategoriesAsync();
    }

    /// <summary>
    /// Allows you to get a list of available categories by serviceId
    /// </summary>
    /// <param name="serviceId">Selected service id</param>
    [Authorize("AuditService.Reference.viewCategories")]
    [HttpGet]
    [Route("categories/{serviceId}")]
    [Produces("application/json", Type = typeof(IDictionary<ServiceId, CategoryDomainModel[]>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<IDictionary<ServiceId, CategoryDomainModel[]>> GetCategoriesAsync(ServiceId serviceId)
    {
        return await _referenceService.GetCategoriesAsync(serviceId);
    }
}