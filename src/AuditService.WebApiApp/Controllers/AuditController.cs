using AuditService.Data.Domain.Dto;
using AuditService.WebApiApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IEnumerable<AuditLogTransactionDto> Log(string name)
    {
        return _auditLog.GetMockedLog();
    }
}