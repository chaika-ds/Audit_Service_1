using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Setup.AppSettings;
using Nest;

namespace AuditService.Handlers.Handlers
{
    /// <summary>
    /// Request handler for receiving audit logs
    /// </summary>
    public class AuditLogRequestHandler : LogRequestBaseHandler<AuditLogFilterDto, AuditLogTransactionDomainModel>
    {
        public AuditLogRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
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
                container &= queryContainerDescriptor.Term(t => t.Service, filter.Service.Value);

            if (filter.NodeId.HasValue)
                container &= queryContainerDescriptor.Term(t => t.NodeId, filter.NodeId.Value);

            if (!string.IsNullOrEmpty(filter.CategoryCode))
                container &= queryContainerDescriptor.Match(t => t.Field(x => x.CategoryCode).Query(filter.CategoryCode));

            if (filter.EntityId.HasValue)
                container &= queryContainerDescriptor.Term(t => t.EntityId, filter.EntityId.Value);

            if (!string.IsNullOrEmpty(filter.Ip))
                container &= queryContainerDescriptor.Match(t => t.Field(x => x.User.Ip).Query(filter.Ip));

            if (!string.IsNullOrEmpty(filter.Login))
                container &= queryContainerDescriptor.Match(t => t.Field(x => x.User.Login).Query(filter.Login));

            if (filter.Action.Any())
                container &= queryContainerDescriptor.Terms(t => t.Field(w => w.Action).Terms(filter.Action));

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
    }
}