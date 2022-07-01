using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Providers.Interfaces;
using AuditService.Utility.Logger.Filters;
using AuditService.Setup.Attributes;
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
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Route("auditlog")]
    [Authorization("Audit.Journal.GetAuditlog")]
    [Produces("application/json", Type = typeof(PageResponseDto<AuditLogTransactionDomainModel>))]
    [TypeFilter(typeof(LoggingActionFilter))]
    public async Task<PageResponseDto<AuditLogTransactionDomainModel>> GetAuditLogAsync([FromQuery] AuditLogFilterRequestDto model, CancellationToken cancellationToken)
    {
        return await _auditLogProcessor.GetAuditLogsByFilterAsync(model, cancellationToken);
    }
}