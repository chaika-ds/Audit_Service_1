using AuditService.WebApiApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
    [HttpGet]
    [Route("services")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IEnumerable<object>> GetServicesAsync()
    {
        throw new UnauthorizedAccessException("resdd");
        return await _referenceProvider.GetServicesAsync();
    }

    /// <summary>
    ///     Allows you to get a list of available categories
    /// </summary>
    [HttpGet]
    [Route("categories")]
    public async Task<IEnumerable<object>> GetCategoriesAsync()
    {
        return await _referenceProvider.GetCategoriesAsync();
    }

    /// <summary>
    ///     Allows you to get a list of available categories by serviceId
    /// </summary>
    /// <param name="serviceId">Selected service id</param>
    [HttpGet]
    [Route("categories/{serviceId:guid}")]
    public async Task<IEnumerable<object>> GetCategoriesAsync(Guid serviceId)
    {
        return await _referenceProvider.GetCategoriesAsync(serviceId);
    }
}