using AuditService.Data.Domain.Dto;
using AuditService.WebApiApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.WebApiApp.Controllers;

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
    public IEnumerable<AuditLogTransactionDto> Log()
    {
        return _auditLog.GetMockedLog();
    }
}