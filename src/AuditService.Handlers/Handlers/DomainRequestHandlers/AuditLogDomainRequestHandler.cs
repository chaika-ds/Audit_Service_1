using AuditService.Common.Extensions;
using AuditService.Common.Models.Domain.AuditLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.PipelineBehaviors.Attributes;
using AuditService.Setup.AppSettings;
using Nest;

namespace AuditService.Handlers.Handlers.DomainRequestHandlers
{
    /// <summary>
    ///     Request handler for receiving audit logs (Domain model)
    /// </summary>
    [UsePipelineBehaviors(UseLogging = true, UseCache = true, CacheLifeTime = 120)]
    public class AuditLogDomainRequestHandler : LogRequestBaseHandler<AuditLogFilterDto, LogSortDto, AuditLogTransactionDomainModel>
    {
        public AuditLogDomainRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// Apply filter to query container
        /// </summary>
        /// <param name="queryContainerDescriptor">Query container descriptor</param>
        /// <param name="filter">The filter model to apply the query</param>
        /// <returns>Query container after applying the filter</returns>
        protected override QueryContainer ApplyFilter(QueryContainerDescriptor<AuditLogTransactionDomainModel> queryContainerDescriptor, AuditLogFilterDto filter)
        {
            var container = new QueryContainer();

            if (filter.Service.HasValue)
                container &= queryContainerDescriptor.Term(t => t.ModuleName, filter.Service.Value);

            if (filter.NodeId.HasValue)
                container &= queryContainerDescriptor.Term(t => t.NodeId, filter.NodeId.Value);

            if (!string.IsNullOrEmpty(filter.CategoryCode))
                container &= queryContainerDescriptor.Match(t => t.Field(x => x.CategoryCode).Query(filter.CategoryCode));

            if (!string.IsNullOrEmpty(filter.EntityId))
                container &= queryContainerDescriptor.Match(t => t.Field(x => x.EntityId).Query(filter.EntityId));

            if (!string.IsNullOrEmpty(filter.Ip))
                container &= queryContainerDescriptor.Match(t => t.Field(x => x.User.Ip).Query(filter.Ip));

            if (!string.IsNullOrEmpty(filter.Login))
                container &= queryContainerDescriptor.Match(t => t.Field(x => x.User.Email).Query(filter.Login));

            if (filter.Action.Any())
                container &= queryContainerDescriptor.Terms(t => t.Field(w => w.ActionName).Terms(filter.Action));

            if (filter.StartDate.HasValue)
                container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.Timestamp).GreaterThan(filter.StartDate.Value));

            if (filter.EndDate.HasValue)
                container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.Timestamp).LessThan(filter.EndDate.Value));

            return container;
        }

        /// <summary>
        /// Get query index
        /// </summary>
        /// <param name="elasticIndexSettings">Elastic index settings</param>
        /// <returns>Query index</returns>
        protected override string? GetQueryIndex(IElasticIndexSettings elasticIndexSettings) =>
            elasticIndexSettings.AuditLog;

        /// <summary>
        /// Get the name of the column to sort
        /// </summary>
        /// <param name="logSortModel">Model to apply sorting</param>
        /// <returns>Column name to sort</returns>
        protected override string GetColumnNameToSort(LogSortDto logSortModel) =>
            nameof(AuditLogTransactionDomainModel.Timestamp).ToCamelCase();
    }
}