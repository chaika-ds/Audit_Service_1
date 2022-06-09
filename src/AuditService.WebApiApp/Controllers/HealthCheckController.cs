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
    /// <returns></returns>
    [HttpGet]
    public IActionResult Index()
    {
        return _healthCheck.CheckElkHealth() && _healthCheck.CheckKafkaHealth()
            ? Ok()
            : StatusCode(500);
    }
}