using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Extensions;
using AuditService.Setup.AppSettings;
using Nest;

namespace AuditService.Handlers.Handlers.DomainRequestHandlers;

/// <summary>
///     Request handler for receiving player changelog (Domain model)
/// </summary>
public class PlayerChangesLogDomainRequestHandler : LogRequestBaseHandler<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogDomainModel>
{
    public PlayerChangesLogDomainRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Apply filter to query container
    /// </summary>
    /// <param name="queryContainerDescriptor">Query container descriptor</param>
    /// <param name="filter">The filter model to apply the query</param>
    /// <returns>Query container after applying the filter</returns>
    protected override QueryContainer ApplyFilter(QueryContainerDescriptor<PlayerChangesLogDomainModel> queryContainerDescriptor, PlayerChangesLogFilterDto filter)
    {
        var container = new QueryContainer();

        if (!string.IsNullOrEmpty(filter.IpAddress))
            container &= queryContainerDescriptor.Match(t => t.Field(x => x.IpAddress).Query(filter.IpAddress));

        if (!string.IsNullOrEmpty(filter.Login))
            container &= queryContainerDescriptor.Match(t => t.Field(x => x.User.Email).Query(filter.Login));

        if (filter.StartDate.HasValue)
            container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.Timestamp).GreaterThan(filter.StartDate.Value));

        if (filter.EndDate.HasValue)
            container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.Timestamp).LessThan(filter.EndDate.Value));

        if (filter.EventKeys.Any())
            container &= queryContainerDescriptor.Terms(t => t.Field(w => w.EventCode.UseSuffix()).Terms(filter.EventKeys));
        
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