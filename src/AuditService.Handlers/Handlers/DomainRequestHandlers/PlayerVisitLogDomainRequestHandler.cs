using AuditService.Common.Enums;
using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Extensions;
using AuditService.Setup.AppSettings;
using Nest;

namespace AuditService.Handlers.Handlers.DomainRequestHandlers;

/// <summary>
///     Request handler for receiving player visit log (Domain model)
/// </summary>
public class PlayerVisitLogDomainRequestHandler : LogRequestBaseHandler<PlayerVisitLogFilterDto, LogColumnSortDto, PlayerVisitLogDomainModel>
{
    public PlayerVisitLogDomainRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Apply filter to query container
    /// </summary>
    /// <param name="queryContainerDescriptor">Query container descriptor</param>
    /// <param name="filter">The filter model to apply the query</param>
    /// <returns>Query container after applying the filter</returns>
    protected override QueryContainer ApplyFilter(QueryContainerDescriptor<PlayerVisitLogDomainModel> queryContainerDescriptor, PlayerVisitLogFilterDto filter)
    {
        var container = new QueryContainer();

        container &= queryContainerDescriptor.Term(t => t.Type, VisitLogType.Player);

        if (filter.HallId.HasValue)
            container &= queryContainerDescriptor.Term(t => t.HallId, filter.HallId.Value);

        if (filter.PlayerId.HasValue)
            container &= queryContainerDescriptor.Term(t => t.PlayerId, filter.PlayerId.Value);

        if (!string.IsNullOrEmpty(filter.Login))
            container &= queryContainerDescriptor.Match(t => t.Field(x => x.Login).Query(filter.Login));

        if (!string.IsNullOrEmpty(filter.Ip))
            container &= queryContainerDescriptor.Match(t => t.Field(x => x.Ip).Query(filter.Ip));

        if (!string.IsNullOrEmpty(filter.OperatingSystem))
            container &= queryContainerDescriptor.Match(t => t.Field(x => x.Authorization.OperatingSystem).Query(filter.OperatingSystem));

        if (!string.IsNullOrEmpty(filter.Browser))
            container &= queryContainerDescriptor.Match(t => t.Field(x => x.Authorization.Browser).Query(filter.Browser));

        if (!string.IsNullOrEmpty(filter.DeviceType))
            container &= queryContainerDescriptor.Match(t => t.Field(x => x.Authorization.DeviceType).Query(filter.DeviceType));

        if (filter.VisitTimeFrom.HasValue)
            container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.Timestamp).GreaterThan(filter.VisitTimeFrom.Value));

        if (filter.VisitTimeTo.HasValue)
            container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.Timestamp).LessThan(filter.VisitTimeTo.Value));

        return container;
    }

    /// <summary>
    ///     Get query index
    /// </summary>
    /// <param name="elasticIndexSettings">Elastic index settings</param>
    /// <returns>Query index</returns>
    protected override string? GetQueryIndex(IElasticIndexSettings elasticIndexSettings) => elasticIndexSettings.VisitLog;

    /// <summary>
    ///     Get the name of the column to sort
    /// </summary>
    /// <param name="logSortModel">Model to apply sorting</param>
    /// <returns>Column name to sort</returns>
    protected override string GetColumnNameToSort(LogColumnSortDto logSortModel) => logSortModel.ColumnName;
}