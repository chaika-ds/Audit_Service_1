using AuditService.Common.Enums;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Setup.AppSettings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using ISort = Nest.ISort;
using sort = AuditService.Common.Models.Dto.Sort;

namespace AuditService.Handlers.Handlers.DomainRequestHandlers
{
    /// <summary>
    /// The base request handler for receiving the log
    /// </summary>
    /// <typeparam name="TFilter">Filter model type</typeparam>
    /// <typeparam name="TSort">Sort model type</typeparam>
    /// <typeparam name="TResponse">Response type</typeparam>
    public abstract class LogDomainRequestBaseHandler<TFilter, TSort, TResponse> : IRequestHandler<LogFilterRequestDto<TFilter, TSort, TResponse>, PageResponseDto<TResponse>>
        where TFilter : class, new()
        where TResponse : class
        where TSort : class, sort.ISort, new()
    {
        private readonly IElasticClient _elasticClient;
        private readonly IElasticIndexSettings _elasticIndexSettings;

        protected LogDomainRequestBaseHandler(IServiceProvider serviceProvider)
        {
            _elasticClient = serviceProvider.GetRequiredService<IElasticClient>();
            _elasticIndexSettings = serviceProvider.GetRequiredService<IElasticIndexSettings>();
        }

        /// <summary>
        /// Apply filter to query container
        /// </summary>
        /// <param name="queryContainerDescriptor">Query container descriptor</param>
        /// <param name="filter">The filter model to apply the query</param>
        /// <returns>Query container after applying the filter</returns>
        protected abstract QueryContainer ApplyFilter(QueryContainerDescriptor<TResponse> queryContainerDescriptor, TFilter filter);

        /// <summary>
        /// Get query index
        /// </summary>
        /// <param name="elasticIndexSettings">Elastic index settings</param>
        /// <returns>Query index</returns>
        protected abstract string? GetQueryIndex(IElasticIndexSettings elasticIndexSettings);

        /// <summary>
        /// Get the name of the column to sort
        /// </summary>
        /// <param name="logSortModel">Model to apply sorting</param>
        /// <returns>Column name to sort</returns>
        protected abstract string GetColumnNameToSort(TSort logSortModel);

        /// <summary>
        /// Apply sorting to query
        /// </summary>
        /// <param name="sortDescriptor">Query sort descriptor</param>
        /// <param name="logSortModel">Model to apply sorting</param>
        /// <returns>Sorted query</returns>
        protected virtual IPromise<IList<ISort>> ApplySorting(SortDescriptor<TResponse> sortDescriptor, TSort logSortModel)
        {
            var columnNameToSort = GetColumnNameToSort(logSortModel);

            return logSortModel.SortableType == SortableType.Ascending
                ? sortDescriptor.Ascending(new Field(columnNameToSort))
                : sortDescriptor.Descending(new Field(columnNameToSort));
        }

        /// <summary>
        /// Handle a request to receive a log in ELK.
        /// </summary>
        /// <param name="request">Log request model</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The model of the result of getting the log</returns>
        public async Task<PageResponseDto<TResponse>> Handle(LogFilterRequestDto<TFilter, TSort, TResponse> request, CancellationToken cancellationToken)
        {
            var response = await _elasticClient.SearchAsync<TResponse>(w => Search(w, request), cancellationToken);
            return new PageResponseDto<TResponse>(request.Pagination, response.HitsMetadata?.Total?.Value ?? 0, response.Documents);
        }

        /// <summary>
        ///  Method for search data in ELK
        /// </summary>
        /// <param name="searchDescriptor">Query search descriptor</param>
        /// <param name="request">Log request model</param>
        /// <returns>Elastic search request</returns>
        private ISearchRequest Search(SearchDescriptor<TResponse> searchDescriptor, LogFilterRequestDto<TFilter, TSort, TResponse> request)
        {
            var query = searchDescriptor
                .From(request.Pagination.GetOffset())
                .Size(request.Pagination.PageSize)
                .Query(w => ApplyFilter(w, request.Filter));

            query = query.Sort(w => ApplySorting(w, request.Sort));

            return query.Index(GetQueryIndex(_elasticIndexSettings));
        }
    }
}