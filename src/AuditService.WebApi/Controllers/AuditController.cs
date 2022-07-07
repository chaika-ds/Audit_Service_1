using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Utility.Logger.Filters;
using AuditService.Setup.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace AuditService.WebApi.Controllers;

/// <summary>
///     Allows you to get a list of audit journals
/// </summary>
[ApiController]
[Route("journal")]
public class AuditController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Allows you to get a list of audit journals
    /// </summary>
    public AuditController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Allows you to get a list of audit logs by filter
    /// </summary>
    /// <param name="model">Filter model</param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Route("auditlog")]
    [Authorization("Audit.Journal.GetAuditlog")]
    [Produces("application/json", Type = typeof(PageResponseDto<AuditLogTransactionDomainModel>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<PageResponseDto<AuditLogTransactionDomainModel>> GetAuditLogAsync(
        [FromQuery] LogFilterRequestDto<AuditLogFilterDto, AuditLogTransactionDomainModel> model, CancellationToken cancellationToken) 
        => await _mediator.Send(model, cancellationToken);
}