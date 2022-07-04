using AuditService.Common.Enums;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Setup.ConfigurationSettings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace AuditService.Handlers.Handlers
{
    /// <summary>
    /// The base request handler for receiving the log
    /// </summary>
    /// <typeparam name="TFilter">Filter model type</typeparam>
    /// <typeparam name="TResponse">Response type</typeparam>
    public abstract class LogRequestBaseHandler<TFilter, TResponse> : IRequestHandler<LogFilterRequestDto<TFilter, TResponse>,
        PageResponseDto<TResponse>> where TFilter : class, new() where TResponse : class
    {
        private readonly IElasticClient _elasticClient;
        private readonly IElasticIndexSettings _elasticIndexSettings;

        protected LogRequestBaseHandler(IServiceProvider serviceProvider)
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
        /// Apply sorting to query
        /// </summary>
        /// <param name="sortDescriptor">Query sort descriptor</param>
        /// <param name="logSortModel">Model to apply sorting</param>
        /// <returns>Sorted query</returns>
        protected virtual IPromise<IList<ISort>> ApplySorting(SortDescriptor<TResponse> sortDescriptor, LogSortDto logSortModel) =>
            logSortModel.SortableType == SortableType.Ascending
                ? sortDescriptor.Ascending(new Field(logSortModel.ColumnName))
                : sortDescriptor.Descending(new Field(logSortModel.ColumnName));

        /// <summary>
        /// Handle a request to receive a log in ELK.
        /// </summary>
        /// <param name="request">Log request model</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The model of the result of getting the log</returns>
        public async Task<PageResponseDto<TResponse>> Handle(LogFilterRequestDto<TFilter, TResponse> request,
            CancellationToken cancellationToken)
        {
            var response = await _elasticClient.SearchAsync<TResponse>(w => Search(w, request), cancellationToken);
            return new PageResponseDto<TResponse>(request.Pagination, response.HitsMetadata?.Total?.Value ?? 0,
                response.Documents);
        }

        /// <summary>
        ///  Method for search data in ELK
        /// </summary>
        /// <param name="searchDescriptor">Query search descriptor</param>
        /// <param name="request">Log request model</param>
        /// <returns>Elastic search request</returns>
        private ISearchRequest Search(SearchDescriptor<TResponse> searchDescriptor, LogFilterRequestDto<TFilter, TResponse> request)
        {
            var query = searchDescriptor
                .From(request.Pagination.PageNumber - 1)
                .Size(request.Pagination.PageSize)
                .Query(w => ApplyFilter(w, request.Filter));

            if (!string.IsNullOrEmpty(request.Sort.ColumnName))
                query = query.Sort(w => ApplySorting(w, request.Sort));

            return query.Index(GetQueryIndex(_elasticIndexSettings));
        }
    }
}