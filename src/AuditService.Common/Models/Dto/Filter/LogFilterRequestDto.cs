using AuditService.Common.Models.Dto.Pagination;
using AuditService.Common.Models.Dto.Sort;
using MediatR;

namespace AuditService.Common.Models.Dto.Filter;

/// <summary>
///     Request to get logs by filter
/// </summary>
/// <typeparam name="TFilter">Filter model type</typeparam>
/// <typeparam name="TSort">Sort model type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam>
public class LogFilterRequestDto<TFilter, TSort, TResponse> : IRequest<PageResponseDto<TResponse>>
    where TFilter : class, new() where TResponse : class
    where TSort : class, ISort, new()
{
    public LogFilterRequestDto()
    {
        Sort = new TSort();
        Filter = new TFilter();
        Pagination = new PaginationRequestDto();
    }

    /// <summary>
    ///     Filter info
    /// </summary>
    public TFilter Filter { get; set; }

    /// <summary>
    ///     Pagination info
    /// </summary>
    public PaginationRequestDto Pagination { get; set; }

    /// <summary>
    ///     Log filter. Sort model
    /// </summary>
    public TSort Sort { get; set; }
}