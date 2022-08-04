using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Common.Models.Dto.Filter.VisitLog;
using Nest;

namespace AuditService.Handlers.Handlers.DomainRequestHandlers.VisitLog;

/// <summary>
///     Base filter for visit log
/// </summary>
internal static class VisitLogBaseFilter
{
    /// <summary>
    ///     Apply basic filtering
    /// </summary>
    /// <typeparam name="TDomainModel">Type of domain visit log model</typeparam>
    /// <param name="container">Query container for applying the filter</param>
    /// <param name="queryContainerDescriptor">Query container descriptor</param>
    /// <param name="filter">The filter model to apply the query</param>
    /// <returns>Query container after applying the filter</returns>
    public static QueryContainer ApplyFilter<TDomainModel>(QueryContainer container, QueryContainerDescriptor<TDomainModel> queryContainerDescriptor, BaseVisitLogFilterDto filter) where TDomainModel : BaseVisitLogDomainModel
    {
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
}