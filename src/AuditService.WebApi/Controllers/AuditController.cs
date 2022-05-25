using AuditService.WebApi.Models;
using AuditService.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuditController : ControllerBase
{
    private readonly IAuditLog _auditLog;
    
    public AuditController(IAuditLog auditLog)
    {
        _auditLog = auditLog;
    }
    
    // GET
    [HttpGet]
    public IEnumerable<KafkaMessage> Log()
    {
        return _auditLog.GetMockedLog();
    }
}