using System.Net;
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
    [ServiceFilter(typeof(LoggingActionFilter))]
    public IActionResult Index()
    {
        bool isElkHealth = _healthCheck.CheckElkHealth();
        bool isKafkaHealth = _healthCheck.CheckKafkaHealth();

        var response = new HealthCheckDto
        {
            Kafka = isKafkaHealth,
            Elk = isElkHealth
        };

        if (isElkHealth && isKafkaHealth) 
            return StatusCode(200, response);
            
        return StatusCode(500, response);

    }
}