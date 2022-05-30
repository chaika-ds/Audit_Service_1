using AuditService.Data.Domain.Dto;
using AuditService.WebApiApp;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.WebApiApp;

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
    public IEnumerable<AuditLogMessageDto> Log()
    {
        return _auditLog.GetMockedLog();
    }
}