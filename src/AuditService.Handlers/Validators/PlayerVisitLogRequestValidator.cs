using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Filter.VisitLog;
using AuditService.Common.Models.Dto.Pagination;
using AuditService.Common.Models.Dto.Sort;
using FluentValidation;

namespace AuditService.Handlers.Validators;

/// <summary>
///     Player visit log request validator
/// </summary>
public class PlayerVisitLogRequestValidator : LogRequestBaseValidator<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel>
{
    public PlayerVisitLogRequestValidator(IValidator<PaginationRequestDto> paginationRequestValidator, IValidator<ILogFilter> logFilterValidator) 
        : base(paginationRequestValidator, logFilterValidator)
    {
    }
}