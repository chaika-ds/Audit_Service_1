using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Utility.Logger.Filters;
using AuditService.Setup.Attributes;
using AuditService.Utility.Logger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.WebApi.Controllers;

/// <summary>
///     Allows you to get a list of available services and categories
/// </summary>
[ApiController]
[Route("reference")]
public class ReferenceController
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Allows you to get a list of available services and categories
    /// </summary>
    public ReferenceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Allows you to get a list of available services
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request</param>
    [HttpGet]
    [Route("services")]
    [Authorization("Audit.Journal.GetAuditlog")]
    [Produces("application/json", Type = typeof(IEnumerable<ServiceStructure>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<IEnumerable<EnumResponseDto>> GetServicesAsync(CancellationToken cancellationToken) 
        => await _mediator.Send(new GetServicesRequest(), cancellationToken);

    /// <summary>
    ///     Allows you to get a list of available categories
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request</param>
    [HttpGet]
    [Route("categories")]
    [Authorization("Audit.Journal.GetAuditlog")]
    [Produces("application/json", Type = typeof(IDictionary<ServiceStructure, CategoryDomainModel[]>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<IDictionary<ServiceStructure, CategoryDomainModel[]>> GetCategoriesAsync(CancellationToken cancellationToken) 
        => await _mediator.Send(new GetCategoriesRequest(), cancellationToken);

    /// <summary>
    ///     Allows you to get a list of available categories by serviceId
    /// </summary>
    /// <param name="service">Selected service id</param>
    /// <param name="cancellationToken">Cancellation token for request</param>
    [HttpGet]
    [Route("categories/{service}")]
    [Authorization("Audit.Journal.GetAuditlog")]
    [Produces("application/json", Type = typeof(IDictionary<ServiceStructure, CategoryDomainModel[]>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<IDictionary<ServiceStructure, CategoryDomainModel[]>> GetCategoriesAsync(ServiceStructure service, CancellationToken cancellationToken) 
        => await _mediator.Send(new GetCategoriesRequest(service), cancellationToken);
}