using System.Net;
using AuditService.Common.Models.Dto;
using AuditService.Utility.Logger;
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
    [HttpGet]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<IActionResult> Index()
    {
        var response = new HealthCheckDto
        {
            Kafka = await _mediator.Send(new CheckKafkaHealthRequest()),
            Elk = await _mediator.Send(new CheckElkHealthRequest())
        };

        return StatusCode(response.IsSuccess() ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError,
            response);
    }
}