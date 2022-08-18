using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Common.Models.Dto.Filter.VisitLog;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.PipelineBehaviors.Attributes;
using AuditService.Setup.AppSettings;
using Nest;

namespace AuditService.Handlers.Handlers.DomainRequestHandlers.VisitLog;

/// <summary>
///     Request handler for receiving player visit log (Domain model)
/// </summary>
[UsePipelineBehaviors(UseCache = true, UseLogging = true, CacheLifeTime = 120, UseValidation = true)]
public class PlayerVisitLogDomainRequestHandler : LogDomainRequestBaseHandler<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel>
{
    public PlayerVisitLogDomainRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Apply filter to query container
    /// </summary>
    /// <param name="container">Query container</param>
    /// <param name="descriptor">Query container descriptor</param>
    /// <param name="filter">The filter model to apply the query</param>
    /// <returns>Query container after applying the filter</returns>
    protected override QueryContainer ApplyFilter(QueryContainer container, QueryContainerDescriptor<PlayerVisitLogDomainModel> descriptor, PlayerVisitLogFilterDto filter)
    {
        container &= descriptor.Match(t => t.Field(x => x.Type).Query(VisitLogType.Player.ToString()));

        if (filter.PlayerId.HasValue)
            container &= descriptor.Term(t => t.PlayerId, filter.PlayerId.Value);

        container &= VisitLogBaseFilter.ApplyFilter(container, descriptor, filter);

        if (!string.IsNullOrEmpty(filter.AuthorizationMethod))
            container &= descriptor.Match(t => t.Field(x => x.Authorization.AuthorizationType).Query(filter.AuthorizationMethod));

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
    protected override string GetColumnNameToSort(PlayerVisitLogSortDto logSortModel) =>
        logSortModel.FieldSortType switch
        {
            PlayerVisitLogSortType.NodeId => nameof(PlayerVisitLogDomainModel.NodeId).ToCamelCase(),
            PlayerVisitLogSortType.PlayerId => nameof(PlayerVisitLogDomainModel.PlayerId).ToCamelCase(),
            PlayerVisitLogSortType.VisitTime => nameof(PlayerVisitLogDomainModel.Timestamp).ToCamelCase(),
            PlayerVisitLogSortType.Login => nameof(PlayerVisitLogDomainModel.Login).ToCamelCase(),
            PlayerVisitLogSortType.Ip => nameof(PlayerVisitLogDomainModel.Ip).ToCamelCase(),
            PlayerVisitLogSortType.Browser => $"{nameof(PlayerVisitLogDomainModel.Authorization).ToCamelCase()}.{nameof(AuthorizationDataDomainModel.Browser).ToCamelCase()}",
            PlayerVisitLogSortType.DeviceType => $"{nameof(PlayerVisitLogDomainModel.Authorization).ToCamelCase()}.{nameof(AuthorizationDataDomainModel.DeviceType).ToCamelCase()}",
            PlayerVisitLogSortType.OperatingSystem => $"{nameof(PlayerVisitLogDomainModel.Authorization).ToCamelCase()}.{nameof(AuthorizationDataDomainModel.OperatingSystem).ToCamelCase()}",
            PlayerVisitLogSortType.AuthorizationMethod => $"{nameof(PlayerVisitLogDomainModel.Authorization).ToCamelCase()}.{nameof(AuthorizationDataDomainModel.AuthorizationType).ToCamelCase()}",
            _ => nameof(PlayerVisitLogDomainModel.Timestamp).ToCamelCase()
        };
}