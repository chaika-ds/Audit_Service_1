using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Setup.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using mediaType = System.Net.Mime.MediaTypeNames.Application;

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
    [Route("auditlog/services")]
    [Authorization("Audit.Journal.GetAuditlog")]
    [Produces(mediaType.Json, Type = typeof(IEnumerable<ModuleName>))]
    public async Task<IEnumerable<EnumResponseDto>> GetServicesAsync(CancellationToken cancellationToken)
        => await _mediator.Send(new GetServicesRequest(), cancellationToken);


    /// <summary>
    ///     Allows you to get a list of available categories
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request</param>
    [HttpGet]
    [Route("auditlog/categories")]
    [Authorization("Audit.Journal.GetAuditlog")]
    [Produces(mediaType.Json, Type = typeof(IDictionary<ModuleName, CategoryDomainModel[]>))]
    public async Task<IDictionary<ModuleName, CategoryDomainModel[]>> GetCategoriesAsync(
        CancellationToken cancellationToken)
        => await _mediator.Send(new GetCategoriesRequest(), cancellationToken);

    /// <summary>
    ///     Allows you to get a list of available categories by serviceId
    /// </summary>
    /// <param name="moduleName">Selected moduleName id</param>
    /// <param name="cancellationToken">Cancellation token for request</param>
    [HttpGet]
    [Route("auditlog/categories/{moduleName}")]
    [Authorization("Audit.Journal.GetAuditlog")]
    [Produces(mediaType.Json, Type = typeof(IDictionary<ModuleName, CategoryDomainModel[]>))]
    public async Task<IDictionary<ModuleName, CategoryDomainModel[]>> GetCategoriesAsync(ModuleName moduleName,
        CancellationToken cancellationToken)
        => await _mediator.Send(new GetCategoriesRequest(moduleName), cancellationToken);

    /// <summary>
    ///     Allows you to get a list of available actions
    /// </summary>
    /// <param name="category">Selected CategoryCode</param>
    /// <param name="cancellationToken">Cancellation token for request</param>
    [HttpGet]
    [Route("auditlog/actions/{category}")]
    [Authorization("Audit.Journal.GetAuditlog")]
    [Produces(mediaType.Json, Type = typeof(IEnumerable<ActionDomainModel>))]
    public async Task<IEnumerable<ActionDomainModel>?> GetActionsAsync(string category, CancellationToken cancellationToken)
        => await _mediator.Send(new GetActionsRequest(category), cancellationToken);
    
    /// <summary>
    ///     Allows you to get a list of available events
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request</param>
    [HttpGet]
    [Route("playerchangeslog/events")]
    [Authorization("Audit.Journal.GetPlayerChangesLog")]
    [Produces(mediaType.Json, Type = typeof(IDictionary<ModuleName, CategoryDomainModel[]>))]
    public async Task<IDictionary<ModuleName, EventDomainModel[]>> GetEventsAsync(CancellationToken cancellationToken)
        => await _mediator.Send(new GetEventsRequest(), cancellationToken);
    
    
    /// <summary>
    ///     Allows you to get a list of available events by Service Id
    /// </summary>
    /// <param name="moduleName">Selected moduleName id</param>
    /// <param name="cancellationToken">Cancellation token for request</param>
    [HttpGet]
    [Route("playerchangeslog/events/{moduleName}")]
    [Authorization("Audit.Journal.GetPlayerChangesLog")]
    [Produces(mediaType.Json, Type = typeof(IDictionary<ModuleName, CategoryDomainModel[]>))]
    public async Task<IDictionary<ModuleName, EventDomainModel[]>> GetEventsAsync(ModuleName moduleName, CancellationToken cancellationToken)
        => await _mediator.Send(new GetEventsRequest(moduleName), cancellationToken);
}