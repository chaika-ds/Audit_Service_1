using AuditService.Common.Contexts;
using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Interfaces;
using AuditService.SettingsService.Commands.BaseEntities;
using AuditService.SettingsService.Commands.GetRootNodeTree;
using AuditService.SettingsService.Extensions;
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
        where TFilter : class, ILogFilter, new()
        where TResponse : class, INodeId
        where TSort : class, sort.ISort, new()
    {
        private readonly IElasticClient _elasticClient;
        private readonly IElasticIndexSettings _elasticIndexSettings;
        private readonly RequestContext _requestContext;
        private readonly SettingsServiceCommands _settingsServiceCommands;

        protected LogDomainRequestBaseHandler(IServiceProvider serviceProvider)
        {
            _elasticClient = serviceProvider.GetRequiredService<IElasticClient>();
            _elasticIndexSettings = serviceProvider.GetRequiredService<IElasticIndexSettings>();
            _requestContext = serviceProvider.GetRequiredService<RequestContext>();
            _settingsServiceCommands = serviceProvider.GetRequiredService<SettingsServiceCommands>();
        }

        /// <summary>
        /// Apply filter to query container
        /// </summary>
        /// <param name="container">Query container</param>
        /// <param name="descriptor">Query container descriptor</param>
        /// <param name="filter">The filter model to apply the query</param>
        /// <returns>Query container after applying the filter</returns>
        protected abstract QueryContainer ApplyFilter(QueryContainer container, QueryContainerDescriptor<TResponse> descriptor, TFilter filter);

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
            var rootNode = await _settingsServiceCommands.GetCommand<IGetRootNodeTreeCommand>().ExecuteAsync(cancellationToken);
            var selectedNodeIds = rootNode.SelectNodeIdsForCurrent(_requestContext.GetRequiredXNodeId());
            var response = await _elasticClient.SearchAsync<TResponse>(w => Search(w, request, selectedNodeIds), cancellationToken);
            return new PageResponseDto<TResponse>(request.Pagination, response.HitsMetadata?.Total?.Value ?? 0, response.Documents);
        }

        /// <summary>
        ///  Method for search data in ELK
        /// </summary>
        /// <param name="searchDescriptor">Query search descriptor</param>
        /// <param name="request">Log request model</param>
        /// <param name="selectedNodeIds">Selected node Ids</param>
        /// <returns>Elastic search request</returns>
        private ISearchRequest Search(SearchDescriptor<TResponse> searchDescriptor, LogFilterRequestDto<TFilter, TSort, TResponse> request, IEnumerable<Guid> selectedNodeIds)
        {
            var indexes = DefineIndexesByTimestamp(request.Filter);
            var query = searchDescriptor
                .From(request.Pagination.GetOffset())
                .Size(request.Pagination.PageSize)
                .Query(w => ApplyFilter(w, request.Filter, selectedNodeIds.ToArray()));

            query = query.Sort(w => ApplySorting(w, request.Sort));
            return query.Index(Indices.Index(indexes));
        }

        /// <summary>
        /// Apply filter to query container
        /// </summary>
        /// <param name="queryContainerDescriptor">Query container descriptor</param>
        /// <param name="filter">The filter model to apply the query</param>
        /// <param name="selectedNodeIds">Selected node Ids</param>
        /// <returns>Query container after applying the filter</returns>
        private QueryContainer ApplyFilter(QueryContainerDescriptor<TResponse> queryContainerDescriptor, TFilter filter, Guid[] selectedNodeIds)
        {
            var container = new QueryContainer();

            if (selectedNodeIds.Any())
                container &= queryContainerDescriptor.Terms(t => t.Field(f => f.NodeId).Terms(selectedNodeIds));

            return ApplyFilter(container, queryContainerDescriptor, filter);
        }

        /// <summary>
        ///     Define indexes by timestamp(filter dates)
        /// </summary>
        /// <param name="logFilter">Log filter</param>
        /// <returns>Indexes</returns>
        private string[] DefineIndexesByTimestamp(ILogFilter logFilter) 
            => logFilter.TimestampFrom.GetTimeIntervalsOfDatesByMonth(logFilter.TimestampTo).Select(date => date.ToElasticIndexFormat(GetQueryIndex(_elasticIndexSettings))).ToArray();
    }
}