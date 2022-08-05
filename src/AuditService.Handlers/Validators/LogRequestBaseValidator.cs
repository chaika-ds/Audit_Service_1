using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Pagination;
using AuditService.Common.Models.Dto.Sort;
using FluentValidation;

namespace AuditService.Handlers.Validators;

/// <summary>
///     Base log request validator
/// </summary>
/// <typeparam name="TFilter">Filter model type</typeparam>
/// <typeparam name="TSort">Sort model type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam>
public abstract class LogRequestBaseValidator<TFilter, TSort, TResponse> : AbstractValidator<LogFilterRequestDto<TFilter, TSort, TResponse>>
    where TFilter : class, new()
    where TResponse : class
    where TSort : class, ISort, new()
{
    protected LogRequestBaseValidator(IValidator<PaginationRequestDto> paginationRequestValidator)
    {
        RuleFor(model => model.Pagination).SetValidator(paginationRequestValidator);
    }
}