using AuditService.Data.Domain.Dto;
using AuditService.Common.Logger;
using AuditService.WebApiApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.WebApiApp.Controllers;

/// <summary>
///     Health check system
/// </summary>
[ApiController]
[Route("_hc")]
[ApiExplorerSettings(IgnoreApi = true)]
public class HealthCheckController : ControllerBase
{
    private readonly IHealthCheck _healthCheck;

    /// <summary>
    ///     Health check system
    /// </summary>
    public HealthCheckController(IHealthCheck healthCheck) => _healthCheck = healthCheck;

    /// <summary>
    ///     Check system
    /// </summary>
    [HttpGet]
    //[ServiceFilter(typeof(LoggingActionFilter))]
    public IActionResult Index()
    {
        var response = new HealthCheckDto
        {
            Kafka = _healthCheck.CheckElkHealth(),
            Elk = _healthCheck.CheckKafkaHealth()
        };

        return response.Kafka && response.Elk ? StatusCode(200, response) : StatusCode(500, response);
    }
}