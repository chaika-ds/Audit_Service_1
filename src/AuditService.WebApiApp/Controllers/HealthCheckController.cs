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

    [TypeFilter(typeof(LoggingActionFilter))]
    [HttpGet]
    public IActionResult Index()
    {
        var response = new HealthCheckDto
        {
            Kafka = _healthCheck.CheckKafkaHealth(),
            Elk = _healthCheck.CheckElkHealth()
        };

        return response.Elk && response.Kafka ? StatusCode(200, response) : StatusCode(500, response);
    }
}