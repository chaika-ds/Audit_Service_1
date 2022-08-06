using AuditService.Common.Models.Dto.Pagination;
using FluentValidation;

namespace AuditService.Handlers.Validators;

/// <summary>
///     Pagination request model validator
/// </summary>
public class PaginationRequestDtoValidator : AbstractValidator<PaginationRequestDto>
{
    public PaginationRequestDtoValidator()
    {
        RuleFor(model => model.PageNumber).Must(pageNumber => pageNumber >= 1)
            .WithMessage($"[{nameof(PaginationRequestDto.PageNumber)}]The page number starts at 1.");

        RuleFor(model => model.PageSize).Must(pageSize => pageSize > 0)
            .WithMessage($"[{nameof(PaginationRequestDto.PageSize)}]The page size must be greater than 0.");
    }
}