using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Providers.Interfaces;
using AuditService.Utility.Logger;
using Microsoft.AspNetCore.Mvc;
using Tolar.Authenticate;
using Tolar.Redis;

namespace AuditService.WebApi.Controllers;

/// <summary>
///     Allows you to get a list of available services and categories
/// </summary>
[ApiController]
[Route("reference")]
public class ReferenceController
{
    private readonly IReferenceProvider _referenceProcessor;
    private readonly IRedisRepository _redis;
    /// <summary>
    ///     Allows you to get a list of available services and categories
    /// </summary>
    public ReferenceController(IReferenceProvider referenceProcessor, IRedisRepository redis)
    {
        _referenceProcessor = referenceProcessor;
        _redis = redis;
    }

    /// <summary>
    ///     Allows you to get a list of available services
    /// </summary>
    [HttpGet]
    [Route("services")]
    [Authorize("AuditService.Reference.viewServices")]
    [Produces("application/json", Type = typeof(IEnumerable<ServiceId>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<IEnumerable<CategoryBaseDomainModel>> GetServicesAsync()
    {
        var value = await _redis.GetAsync<IEnumerable<CategoryBaseDomainModel>>("Reference.GetServices");

        if (value != null) return value;
        
        var response = await _referenceProcessor.GetServicesAsync();

        var cacheValue = response.ToList();
       
        await _redis.SetAsync("Reference.GetServices", cacheValue, TimeSpan.FromMinutes(10));
        
        return cacheValue;
    }

    /// <summary>
    ///     Allows you to get a list of available categories
    /// </summary>
    [HttpGet]
    [Route("categories")]
    [Authorize("AuditService.Reference.viewCategories")]
    [Produces("application/json", Type = typeof(IDictionary<ServiceId, CategoryDomainModel[]>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<IDictionary<ServiceId, CategoryDomainModel[]>> GetCategoriesAsync()
    {
        var value = await _redis.GetAsync<IDictionary<ServiceId, CategoryDomainModel[]>>("Reference.GetCategories");

        if (value != null) return value;

        var response = await _referenceProcessor.GetCategoriesAsync();
       
        await _redis.SetAsync("Reference.GetCategories", response, TimeSpan.FromMinutes(10));
        
        return response;
    }

    /// <summary>
    ///     Allows you to get a list of available categories by serviceId
    /// </summary>
    /// <param name="serviceId">Selected service id</param>
    [HttpGet]
    [Route("categories/{serviceId}")]
    [Authorize("AuditService.Reference.viewCategories")]
    [Produces("application/json", Type = typeof(IDictionary<ServiceId, CategoryDomainModel[]>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<IDictionary<ServiceId, CategoryDomainModel[]>> GetCategoriesAsync(ServiceId serviceId)
    {
        var value = await _redis.GetAsync<IDictionary<ServiceId, CategoryDomainModel[]>>($"Reference.GetCategories.{serviceId}");

        if (value != null) return value;

        var response = await _referenceProcessor.GetCategoriesAsync(serviceId);
       
        await _redis.SetAsync($"Reference.GetCategories.{serviceId}", response, TimeSpan.FromMinutes(10));
        
        return response;
    }
}