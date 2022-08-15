using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Pagination;
using AuditService.Common.Models.Dto.Sort;
using FluentValidation;

namespace AuditService.Handlers.Validators;

/// <summary>
///     Losses log request validator
/// </summary>
public class LossesLogRequestValidator : LogRequestBaseValidator<LossesLogFilterDto, LossesLogSortDto, LossesLogResponseDto>
{
    public LossesLogRequestValidator(IValidator<PaginationRequestDto> paginationRequestValidator, IValidator<ILogFilter> logFilterValidator) : base(paginationRequestValidator, logFilterValidator)
    {
    }
}