using AuditService.Data.Domain.Domain;
using AuditService.Data.Domain.Dto;
using AuditService.Data.Domain.Dto.Filter;
using AuditService.WebApiApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    [Produces("application/json", Type = typeof(PageResponseDto<AuditLogTransactionDomainModel>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<PageResponseDto<AuditLogTransactionDomainModel>> GetLogAsync([FromQuery] AuditLogFilterRequestDto model)
    {
        return await _auditLogService.GetLogsByFilterAsync(model);
    }
}