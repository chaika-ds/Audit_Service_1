using AuditService.Common.Models.Dto;
using AuditService.Providers.Interfaces;
using AuditService.Utility.Logger.Filters;
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
    private readonly IHealthCheckProvider _healthCheckProvider;

    public HealthCheckController(IHealthCheckProvider healthCheckProvider)
    {
        _healthCheckProvider = healthCheckProvider;
    }

    /// <summary>
    ///     PING
    /// </summary>
    [HttpGet]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var response = new HealthCheckDto
        {
            Kafka = _healthCheckProvider.CheckKafkaHealth(), 
            Elk = await _healthCheckProvider.CheckElkHealthAsync(cancellationToken)
        };

        return StatusCode(response.IsSuccess() ? 200 : 500, response);
    }
}