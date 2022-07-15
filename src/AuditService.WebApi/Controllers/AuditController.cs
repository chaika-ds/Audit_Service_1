using AuditService.Common.Models.Domain.AuditLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Setup.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using mediaType = System.Net.Mime.MediaTypeNames.Application;


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
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpGet]
    [Route("auditlog")]
    [Authorization("Audit.Journal.GetAuditlog")]
    [Produces(mediaType.Json, Type = typeof(PageResponseDto<AuditLogTransactionDomainModel>))]
    public async Task<PageResponseDto<AuditLogTransactionDomainModel>> GetAuditLogAsync(
        [FromQuery] LogFilterRequestDto<AuditLogFilterDto, LogSortDto, AuditLogTransactionDomainModel> model, CancellationToken cancellationToken) 
        => await _mediator.Send(model, cancellationToken);

    /// <summary>
    ///     Allows you to get a list of player card logchanges by filter
    /// </summary>
    /// <param name="request">Request to get the player card changelog</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpGet]
    [Route("playerChangesLog")]
    [Authorization("Audit.Journal.GetPlayerChangesLog")]
    [Produces(mediaType.Json, Type = typeof(PageResponseDto<PlayerChangesLogResponseDto>))]
    public async Task<PageResponseDto<PlayerChangesLogResponseDto>> GetPlayerChangesLogAsync(
        [FromQuery] LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogResponseDto> request, CancellationToken cancellationToken)
        => await _mediator.Send(request, cancellationToken);


    /// <summary>
    ///     Allows you to get a list of blocked players log by filter
    /// </summary>
    /// <param name="request">Request to get the blocked players log</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpGet]
    [Route("blockedPlayersLog")]
    [Authorization("Audit.Journal.GetBlockedPlayersLog")]
    [Produces(mediaType.Json, Type = typeof(PageResponseDto<BlockedPlayersLogResponseDto>))]
    public async Task<PageResponseDto<BlockedPlayersLogResponseDto>> GetBlockedPlayersLogAsync(
        [FromQuery] LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogResponseDto> request, CancellationToken cancellationToken)
        => await _mediator.Send(request, cancellationToken);
}