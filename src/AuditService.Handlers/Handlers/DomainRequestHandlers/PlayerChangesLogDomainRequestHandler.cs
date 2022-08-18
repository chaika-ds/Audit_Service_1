using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Setup.AppSettings;
using Nest;

namespace AuditService.Handlers.Handlers.DomainRequestHandlers;

/// <summary>
///     Request handler for receiving player changelog (Domain model)
/// </summary>
public class PlayerChangesLogDomainRequestHandler : LogDomainRequestBaseHandler<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogDomainModel>
{
    public PlayerChangesLogDomainRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Apply filter to query container
    /// </summary>
    /// <param name="container">Query container</param>
    /// <param name="descriptor">Query container descriptor</param>
    /// <param name="filter">The filter model to apply the query</param>
    /// <returns>Query container after applying the filter</returns>
    protected override QueryContainer ApplyFilter(QueryContainer container, QueryContainerDescriptor<PlayerChangesLogDomainModel> descriptor, PlayerChangesLogFilterDto filter)
    {
        if (!string.IsNullOrEmpty(filter.IpAddress))
            container &= descriptor.Match(t => t.Field(x => x.IpAddress).Query(filter.IpAddress));

        container &= descriptor.Term(t => t.PlayerId, filter.PlayerId);
        container &= descriptor.DateRange(t => t.Field(w => w.Timestamp).GreaterThan(filter.TimestampFrom));
        container &= descriptor.DateRange(t => t.Field(w => w.Timestamp).LessThan(filter.TimestampTo));

        if (filter.EventKeys.Any())
            container &= descriptor.Terms(t => t.Field(w => w.EventCode).Terms(filter.EventKeys));
        
        return container;
    }

    /// <summary>
    ///     Get query index
    /// </summary>
    /// <param name="elasticIndexSettings">Elastic index settings</param>
    /// <returns>Query index</returns>
    protected override string? GetQueryIndex(IElasticIndexSettings elasticIndexSettings) =>
        elasticIndexSettings.PlayerChangesLog;

    /// <summary>
    ///     Get the name of the column to sort
    /// </summary>
    /// <param name="logSortModel">Model to apply sorting</param>
    /// <returns>Column name to sort</returns>
    protected override string GetColumnNameToSort(LogSortDto logSortModel) =>
        nameof(PlayerChangesLogDomainModel.Timestamp).ToLower();
}