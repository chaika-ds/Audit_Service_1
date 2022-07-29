using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Common.Models.Dto.Filter.VisitLog;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Consts;
using AuditService.Handlers.PipelineBehaviors.Attributes;
using AuditService.Setup.AppSettings;
using Nest;
using ISort = Nest.ISort;

namespace AuditService.Handlers.Handlers.DomainRequestHandlers;

/// <summary>
///     Request handler for receiving user visit log (Domain model)
/// </summary>
[UsePipelineBehaviors(UseCache = true, UseLogging = true, CacheLifeTime = 120)]
public class UserVisitLogDomainRequestHandler : LogRequestBaseHandler<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogDomainModel>
{
    public UserVisitLogDomainRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Apply filter to query container
    /// </summary>
    /// <param name="queryContainerDescriptor">Query container descriptor</param>
    /// <param name="filter">The filter model to apply the query</param>
    /// <returns>Query container after applying the filter</returns>
    protected override QueryContainer ApplyFilter(QueryContainerDescriptor<UserVisitLogDomainModel> queryContainerDescriptor, UserVisitLogFilterDto filter)
    {
        var container = new QueryContainer();

        container &= queryContainerDescriptor.Term(t => t.Type, VisitLogType.User);

        if (filter.NodeId.HasValue)
            container &= queryContainerDescriptor.Term(t => t.NodeId, filter.NodeId.Value);

        if (filter.UserId.HasValue)
            container &= queryContainerDescriptor.Term(t => t.UserId, filter.UserId.Value);

        if (!string.IsNullOrEmpty(filter.UserRole))
            container &= queryContainerDescriptor.Match(t => t.Field(new Field(GetNameOfUserRoleNameField())).Query(filter.UserRole));

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
    ///     Apply sorting to query
    /// </summary>
    /// <param name="sortDescriptor">Query sort descriptor</param>
    /// <param name="logSortModel">Model to apply sorting</param>
    /// <returns>Sorted query</returns>
    protected override IPromise<IList<ISort>> ApplySorting(SortDescriptor<UserVisitLogDomainModel> sortDescriptor, UserVisitLogSortDto logSortModel)
        => logSortModel.FieldSortType switch
        {
            UserVisitLogSortType.Login => sortDescriptor.Field(field => field.Login.Suffix(ElasticConst.SuffixKeyword), (SortOrder)logSortModel.SortableType),
            UserVisitLogSortType.Ip => sortDescriptor.Field(field => field.Ip.Suffix(ElasticConst.SuffixKeyword), (SortOrder)logSortModel.SortableType),
            UserVisitLogSortType.Browser => sortDescriptor.Field(field => field.Authorization.Browser.Suffix(ElasticConst.SuffixKeyword), (SortOrder)logSortModel.SortableType),
            UserVisitLogSortType.DeviceType => sortDescriptor.Field(field => field.Authorization.DeviceType.Suffix(ElasticConst.SuffixKeyword), (SortOrder)logSortModel.SortableType),
            UserVisitLogSortType.OperatingSystem => sortDescriptor.Field(field => field.Authorization.OperatingSystem.Suffix(ElasticConst.SuffixKeyword), (SortOrder)logSortModel.SortableType),
            _ => base.ApplySorting(sortDescriptor, logSortModel)
        };

    /// <summary>
    ///     Get the name of the column to sort
    /// </summary>
    /// <param name="logSortModel">Model to apply sorting</param>
    /// <returns>Column name to sort</returns>
    protected override string GetColumnNameToSort(UserVisitLogSortDto logSortModel) =>
        logSortModel.FieldSortType switch
        {
            UserVisitLogSortType.NodeId => nameof(UserVisitLogDomainModel.NodeId).ToCamelCase(),
            UserVisitLogSortType.VisitTime => nameof(UserVisitLogDomainModel.Timestamp).ToCamelCase(),
            _ => nameof(UserVisitLogDomainModel.Timestamp).ToCamelCase()
        };

    /// <summary>
    ///     Get the name of the user role name field
    /// </summary>
    /// <returns>Name of the user role code field</returns>
    private static string GetNameOfUserRoleNameField() =>
        $"{nameof(UserVisitLogDomainModel.UserRoles).ToCamelCase()}.{nameof(UserRoleDomainModel.Name).ToCamelCase()}";
}