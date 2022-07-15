using System.Net;
using AuditService.Common.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.WebApi.Controllers;

/// <summary>
///     Health check service
/// </summary>
[ApiController]
[Route("_hc")]
[ApiExplorerSettings(IgnoreApi = true)]
public class HealthCheckController : ControllerBase
{
    private readonly IMediator _mediator;

    public HealthCheckController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     PING
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for request</param>
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var response = new HealthCheckDto
        {
            Kafka = await _mediator.Send(new CheckKafkaHealthRequest(), cancellationToken),
            Elk = await _mediator.Send(new CheckElkHealthRequest(), cancellationToken),
            Redis = await _mediator.Send(new CheckRedisHealthRequest(), cancellationToken)
        };

        return StatusCode(response.IsSuccess() ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError, response);
    }
}