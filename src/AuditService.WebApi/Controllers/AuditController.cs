using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Providers.Interfaces;
using AuditService.Utility.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.WebApi.Controllers;

/// <summary>
///     Allows you to get a list of audit journals
/// </summary>
[ApiController]
[Route("journal")]
public class AuditController : ControllerBase
{
    private readonly IAuditLogProvider _auditLogProcessor;

    /// <summary>
    ///     Allows you to get a list of audit journals
    /// </summary>
    public AuditController(IAuditLogProvider auditLogProcessor)
    {
        _auditLogProcessor = auditLogProcessor;
    }

    /// <summary>
    ///     Allows you to get a list of audit logs by filter
    /// </summary>
    /// <param name="model">Filter model</param>
    [HttpGet]
    [Route("auditlog")]
    [Authorize("AuditService.Journal.viewAuditLogsByFilter")]
    [Produces("application/json", Type = typeof(PageResponseDto<AuditLogTransactionDomainModel>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<PageResponseDto<AuditLogTransactionDomainModel>> GetAuditLogAsync([FromQuery] AuditLogFilterRequestDto model)
    {
        return await _auditLogProcessor.GetAuditLogsByFilterAsync(model);
    }
}