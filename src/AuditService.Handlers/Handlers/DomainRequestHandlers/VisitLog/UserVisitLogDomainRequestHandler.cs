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
///     Request handler for receiving user visit log (Domain model)
/// </summary>
[UsePipelineBehaviors(UseCache = true, UseLogging = true, CacheLifeTime = 120, UseValidation = true)]
public class UserVisitLogDomainRequestHandler : LogDomainRequestBaseHandler<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogDomainModel>
{
    public UserVisitLogDomainRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Apply filter to query container
    /// </summary>
    /// <param name="container">Query container</param>
    /// <param name="descriptor">Query container descriptor</param>
    /// <param name="filter">The filter model to apply the query</param>
    /// <returns>Query container after applying the filter</returns>
    protected override QueryContainer ApplyFilter(QueryContainer container, QueryContainerDescriptor<UserVisitLogDomainModel> descriptor, UserVisitLogFilterDto filter)
    {
        container &= descriptor.Match(t => t.Field(x => x.Type).Query(VisitLogType.User.ToString()));

        if (filter.UserId.HasValue)
            container &= descriptor.Term(t => t.UserId, filter.UserId.Value);

        if (!string.IsNullOrEmpty(filter.UserRole))
            container &= descriptor.Match(t => t.Field(new Field(GetNameOfUserRoleNameField())).Query(filter.UserRole));

        container &= VisitLogBaseFilter.ApplyFilter(container, descriptor, filter);

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
    protected override string GetColumnNameToSort(UserVisitLogSortDto logSortModel) =>
        logSortModel.FieldSortType switch
        {
            UserVisitLogSortType.NodeId => nameof(UserVisitLogDomainModel.NodeId).ToCamelCase(),
            UserVisitLogSortType.VisitTime => nameof(UserVisitLogDomainModel.Timestamp).ToCamelCase(),
            UserVisitLogSortType.Login => nameof(UserVisitLogDomainModel.Login).ToCamelCase(),
            UserVisitLogSortType.Ip => nameof(UserVisitLogDomainModel.Ip).ToCamelCase(),
            UserVisitLogSortType.Browser => $"{nameof(UserVisitLogDomainModel.Authorization).ToCamelCase()}.{nameof(AuthorizationDataDomainModel.Browser).ToCamelCase()}",
            UserVisitLogSortType.DeviceType => $"{nameof(UserVisitLogDomainModel.Authorization).ToCamelCase()}.{nameof(AuthorizationDataDomainModel.DeviceType).ToCamelCase()}",
            UserVisitLogSortType.OperatingSystem => $"{nameof(UserVisitLogDomainModel.Authorization).ToCamelCase()}.{nameof(AuthorizationDataDomainModel.OperatingSystem).ToCamelCase()}",
            _ => nameof(UserVisitLogDomainModel.Timestamp).ToCamelCase()
        };

    /// <summary>
    ///     Get the name of the user role name field
    /// </summary>
    /// <returns>Name of the user role code field</returns>
    private static string GetNameOfUserRoleNameField() =>
        $"{nameof(UserVisitLogDomainModel.UserRoles).ToCamelCase()}.{nameof(UserRoleDomainModel.Name).ToCamelCase()}";
}