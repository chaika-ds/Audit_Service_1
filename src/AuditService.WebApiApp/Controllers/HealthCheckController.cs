using AuditService.Common.ELK;
using AuditService.Common.Health;
using AuditService.WebApiApp.Services.Interfaces;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nest;

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

    [HttpGet]
    public IActionResult Index()
    {
          return _healthCheck.CheckElkHealth() && _healthCheck.CheckKafkaHealth()
            ? Ok()
            : StatusCode(500);
    }
}