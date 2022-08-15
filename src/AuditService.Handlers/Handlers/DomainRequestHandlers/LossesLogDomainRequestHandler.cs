using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Common.Models.Domain.LossesLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Setup.AppSettings;
using Nest;

namespace AuditService.Handlers.Handlers.DomainRequestHandlers;

/// <summary>
///     Request handler for receiving losses log (Domain model)
/// </summary>
public class LossesLogDomainRequestHandler : LogDomainRequestBaseHandler<LossesLogFilterDto, LossesLogSortDto, LossesLogDomainModel>
{
    public LossesLogDomainRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Apply filter to query container
    /// </summary>
    /// <param name="container">Query container</param>
    /// <param name="descriptor">Query container descriptor</param>
    /// <param name="filter">The filter model to apply the query</param>
    /// <returns>Query container after applying the filter</returns>
    protected override QueryContainer ApplyFilter(QueryContainer container, QueryContainerDescriptor<LossesLogDomainModel> descriptor, LossesLogFilterDto filter)
    {
        container &= descriptor.DateRange(t => t.Field(w => w.CreateDate).GreaterThan(filter.TimestampFrom));
        container &= descriptor.DateRange(t => t.Field(w => w.CreateDate).LessThan(filter.TimestampTo));

        if (filter.PlayerId.HasValue)
            container &= descriptor.Term(t => t.PlayerId, filter.PlayerId.Value);

        if (filter.NodeId.HasValue)
            container &= descriptor.Term(t => t.NodeId, filter.NodeId.Value);

        if (!string.IsNullOrEmpty(filter.Login))
            container &= descriptor.Match(t => t.Field(x => x.Login).Query(filter.Login));

        if (!string.IsNullOrEmpty(filter.CurrencyCode))
            container &= descriptor.Match(t => t.Field(x => x.CurrencyCode).Query(filter.CurrencyCode));

        if (filter.LastDeposit.HasValue)
            container &= descriptor.Term(t => t.LastDeposit, filter.LastDeposit.Value);

        return container;
    }

    /// <summary>
    ///     Get query index
    /// </summary>
    /// <param name="elasticIndexSettings">Elastic index settings</param>
    /// <returns>Query index</returns>
    protected override string? GetQueryIndex(IElasticIndexSettings elasticIndexSettings) =>
        elasticIndexSettings.LossesLog;

    /// <summary>
    ///     Get the name of the column to sort
    /// </summary>
    /// <param name="logSortModel">Model to apply sorting</param>
    /// <returns>Column name to sort</returns>
    protected override string GetColumnNameToSort(LossesLogSortDto logSortModel) =>
        logSortModel.FieldSortType switch
        {
            LossesLogSortType.CreatedTime => nameof(LossesLogDomainModel.CreateDate).ToCamelCase(),
            LossesLogSortType.LastDeposit => nameof(LossesLogDomainModel.LastDeposit).ToCamelCase(),
            _ => nameof(LossesLogDomainModel.CreateDate).ToCamelCase()
        };
}