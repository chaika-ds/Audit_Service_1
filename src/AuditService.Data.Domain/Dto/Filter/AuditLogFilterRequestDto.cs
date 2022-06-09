using AuditService.Data.Domain.Dto.Pagination;

namespace AuditService.Data.Domain.Dto.Filter;

/// <summary>
///     Audit log filter. Request
/// </summary>
public class AuditLogFilterRequestDto
{
    /// <summary>
    ///     Audit log filter. Request
    /// </summary>
    public AuditLogFilterRequestDto()
    {
        Sort = new AuditLogSortDto();
        Filter = new AuditLogFilterDto();
        Pagination = new PaginationRequestDto();
    }

    /// <summary>
    ///     Filter info
    /// </summary>
    public AuditLogFilterDto Filter { get; set; }

    /// <summary>
    ///     Pagination info
    /// </summary>
    public PaginationRequestDto Pagination { get; set; }

    /// <summary>
    ///     Audit log filter. Sort model
    /// </summary>
    public AuditLogSortDto Sort { get; set; }
}