using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Pagination;
using AuditService.Common.Models.Dto.Sort;
using FluentValidation;

namespace AuditService.Handlers.Validators;

/// <summary>
///     Player changes log request validator
/// </summary>
public class PlayerChangesLogRequestValidator : LogRequestBaseValidator<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogDomainModel>
{
    public PlayerChangesLogRequestValidator(IValidator<PaginationRequestDto> paginationRequestValidator, IValidator<ILogFilter> logFilterValidator) 
        : base(paginationRequestValidator, logFilterValidator)
    {
    }
}