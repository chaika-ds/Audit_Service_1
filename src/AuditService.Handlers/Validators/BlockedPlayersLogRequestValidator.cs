using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Pagination;
using AuditService.Common.Models.Dto.Sort;
using FluentValidation;

namespace AuditService.Handlers.Validators;

/// <summary>
///     Blocked players log request validator
/// </summary>
public class BlockedPlayersLogRequestValidator : LogRequestBaseValidator<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogDomainModel>
{
    public BlockedPlayersLogRequestValidator(IValidator<PaginationRequestDto> paginationRequestValidator, IValidator<ILogFilter> logFilterValidator) 
        : base(paginationRequestValidator, logFilterValidator)
    {
    }
}