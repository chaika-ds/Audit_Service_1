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
        return _healthCheck.CheckElkHealth() && _healthCheck.CheckKafkaHealth()
            ? Ok()
            : StatusCode(500);
    }
}