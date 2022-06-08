using System.Net;
using AuditService.Data.Domain.Dto;
using AuditService.Common.Logger;
using AuditService.WebApiApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.WebApiApp.Controllers;

[ApiController]
[Route("_hc")]
public class HealthCheckController : ControllerBase
{
    private readonly IHealthCheck _healthCheck;

    public HealthCheckController(IHealthCheck healthCheck)
    {
        _healthCheck = healthCheck;
    }

    [ServiceFilter(typeof(LoggingActionFilter))]
    [HttpGet]
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