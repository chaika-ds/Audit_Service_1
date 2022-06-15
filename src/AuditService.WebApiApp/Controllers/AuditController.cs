using AuditService.Utility.Logger;
using AuditService.Data.Domain.Dto;
using AuditService.Data.Domain.Dto.Filter;
using AuditService.WebApiApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AuditService.Data.Domain.Domain;
using Microsoft.AspNetCore.Authorization;

namespace AuditService.WebApiApp.Controllers;

/// <summary>
///     Allows you to get a list of audit logs
/// </summary>
[ApiController]
[Route("audit")]
public class AuditController : ControllerBase
{
    private readonly IAuditLogService _auditLogService;

    /// <summary>
    ///     Allows you to get a list of audit logs
    /// </summary>
    public AuditController(IAuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }

    /// <summary>
    ///     Allows you to get a list of audit logs by filter
    /// </summary>
    /// <param name="model">Filter model</param>
    [HttpGet]
    [Route("log")]
    [Authorize("AuditService.Audit.viewLogsByFilter")]
    [Produces("application/json", Type = typeof(PageResponseDto<AuditLogTransactionDomainModel>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<PageResponseDto<AuditLogTransactionDomainModel>> GetLogAsync([FromQuery] AuditLogFilterRequestDto model)
    {
        return await _auditLogService.GetLogsByFilterAsync(model);
    }
}