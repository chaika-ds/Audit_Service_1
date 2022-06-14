using AuditService.Data.Domain.Domain;
using AuditService.Data.Domain.Dto;
using AuditService.Data.Domain.Dto.Filter;

namespace AuditService.WebApiApp.Services.Interfaces;

/// <summary>
///     Audit log provider
/// </summary>
public interface IAuditLogService
{
    /// <summary>
    ///     Get audit logs by filter
    /// </summary>
    /// <param name="filter">Filter model</param>
    Task<PageResponseDto<AuditLogTransactionDomainModel>> GetLogsByFilterAsync(AuditLogFilterRequestDto filter);
}