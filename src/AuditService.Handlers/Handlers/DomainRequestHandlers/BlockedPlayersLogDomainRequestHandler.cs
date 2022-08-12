using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Consts;
using AuditService.Handlers.PipelineBehaviors.Attributes;
using AuditService.Setup.AppSettings;
using Nest;
using ISort = Nest.ISort;

namespace AuditService.Handlers.Handlers.DomainRequestHandlers;

/// <summary>
///     Request handler for receiving blocked players log (Domain model)
/// </summary>
[UsePipelineBehaviors(UseLogging = true, UseCache = true, CacheLifeTime = 120, UseValidation = true)]
public class BlockedPlayersLogDomainRequestHandler : LogDomainRequestBaseHandler<BlockedPlayersLogFilterDto,
    BlockedPlayersLogSortDto, BlockedPlayersLogDomainModel>
{
    public BlockedPlayersLogDomainRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Apply filter to query container
    /// </summary>
    /// <param name="container">Query container</param>
    /// <param name="descriptor">Query container descriptor</param>
    /// <param name="filter">The filter model to apply the query</param>
    /// <returns>Query container after applying the filter</returns>
    protected override QueryContainer ApplyFilter(QueryContainer container, QueryContainerDescriptor<BlockedPlayersLogDomainModel> descriptor, BlockedPlayersLogFilterDto filter)
    {
        container &= descriptor.DateRange(t => t.Field(w => w.BlockingDate).GreaterThan(filter.TimestampFrom));
        container &= descriptor.DateRange(t => t.Field(w => w.BlockingDate).LessThan(filter.TimestampTo));

        if (filter.PreviousBlockingDateFrom.HasValue)
            container &= descriptor.DateRange(t => t.Field(w => w.PreviousBlockingDate).GreaterThan(filter.PreviousBlockingDateFrom.Value));

        if (filter.PreviousBlockingDateTo.HasValue)
            container &= descriptor.DateRange(t => t.Field(w => w.PreviousBlockingDate).LessThan(filter.PreviousBlockingDateTo.Value));

        if (!string.IsNullOrEmpty(filter.PlayerLogin))
            container &= descriptor.Match(t => t.Field(x => x.PlayerLogin).Query(filter.PlayerLogin));

        if (filter.PlayerId.HasValue)
            container &= descriptor.Term(t => t.PlayerId.Suffix(ElasticConst.SuffixKeyword), filter.PlayerId.Value);

        if (!string.IsNullOrEmpty(filter.PlayerIp))
            container &= descriptor.Match(t => t.Field(x => x.LastVisitIpAddress).Query(filter.PlayerIp));

        if (filter.NodeId.HasValue)
            container &= descriptor.Term(t => t.NodeId.Suffix(ElasticConst.SuffixKeyword), filter.NodeId.Value);

        if (!string.IsNullOrEmpty(filter.Platform))
            container &= descriptor.Match(t => t.Field(x => x.Platform).Query(filter.Platform));

        if (!string.IsNullOrEmpty(filter.Browser))
            container &= descriptor.Match(t => t.Field(x => x.Browser).Query(filter.Browser));

        if (!string.IsNullOrEmpty(filter.Version))
            container &= descriptor.Match(t => t.Field(x => x.BrowserVersion).Query(filter.Version));

        if (!string.IsNullOrEmpty(filter.Language))
            container &= descriptor.Match(t => t.Field(x => x.Language).Query(filter.Language));

        return container;
    }

    /// <summary>
    ///     Get query index
    /// </summary>
    /// <param name="elasticIndexSettings">Elastic index settings</param>
    /// <returns>Query index</returns>
    protected override string? GetQueryIndex(IElasticIndexSettings elasticIndexSettings) =>
        elasticIndexSettings.BlockedPlayersLog;

    /// <summary>
    ///     Apply sorting to query
    /// </summary>
    /// <param name="sortDescriptor">Query sort descriptor</param>
    /// <param name="logSortModel">Model to apply sorting</param>
    /// <returns>Sorted query</returns>
    protected override IPromise<IList<ISort>> ApplySorting(SortDescriptor<BlockedPlayersLogDomainModel> sortDescriptor, BlockedPlayersLogSortDto logSortModel)
        => logSortModel.FieldSortType switch
        {
            BlockedPlayersLogSortType.Version => sortDescriptor.Field(field => field.BrowserVersion.Suffix(ElasticConst.SuffixKeyword), (SortOrder)logSortModel.SortableType),
            _ => base.ApplySorting(sortDescriptor, logSortModel)
        };

    /// <summary>
    ///     Get the name of the column to sort
    /// </summary>
    /// <param name="logSortModel">Model to apply sorting</param>
    /// <returns>Column name to sort</returns>
    protected override string GetColumnNameToSort(BlockedPlayersLogSortDto logSortModel) =>
        logSortModel.FieldSortType switch
        {
            BlockedPlayersLogSortType.BlockingDate => nameof(BlockedPlayersLogDomainModel.BlockingDate).ToCamelCase(),
            BlockedPlayersLogSortType.BlocksCounter => nameof(BlockedPlayersLogDomainModel.BlocksCounter).ToCamelCase(),
            BlockedPlayersLogSortType.PreviousBlockingDate => nameof(BlockedPlayersLogDomainModel.PreviousBlockingDate).ToCamelCase(),
            BlockedPlayersLogSortType.Version => nameof(BlockedPlayersLogDomainModel.BrowserVersion).ToCamelCase(),
            _ => nameof(BlockedPlayersLogDomainModel.Timestamp).ToCamelCase()
        };
}