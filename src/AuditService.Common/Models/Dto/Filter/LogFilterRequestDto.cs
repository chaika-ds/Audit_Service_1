using AuditService.Common.Models.Dto.Pagination;
using MediatR;

namespace AuditService.Common.Models.Dto.Filter
{
    /// <summary>
    /// Request to get logs by filter
    /// </summary>
    /// <typeparam name="TFilter">Filter model type</typeparam>
    /// <typeparam name="TResponse">Response type</typeparam>
    public class LogFilterRequestDto<TFilter, TResponse> : IRequest<PageResponseDto<TResponse>>
        where TFilter : class, new() where TResponse : class
    {
        public LogFilterRequestDto()
        {
            Sort = new LogSortDto();
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
        ///     Audit log filter. Sort model
        /// </summary>
        public LogSortDto Sort { get; set; }
    }
}