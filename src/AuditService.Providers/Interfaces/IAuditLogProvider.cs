using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;

namespace AuditService.Providers.Interfaces;

/// <summary>
///     Audit log provider
/// </summary>
public interface IAuditLogProvider
{
    /// <summary>
    ///     Get audit logs by filter
    /// </summary>
    /// <param name="filter">Filter model</param>
    Task<PageResponseDto<AuditLogTransactionDomainModel>> GetAuditLogsByFilterAsync(AuditLogFilterRequestDto filter);
}