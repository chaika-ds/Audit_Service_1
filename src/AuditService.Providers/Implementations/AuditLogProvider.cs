using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Providers.Interfaces;
using AuditService.Setup.ConfigurationSettings;
using Nest;

namespace AuditService.Providers.Implementations;

/// <summary>
///     Audit log processor
/// </summary>
public class AuditLogProvider : IAuditLogProvider
{
    private readonly IElasticClient _elasticClient;
    private readonly IElasticIndexSettings _elasticIndexSettings;

    public AuditLogProvider(IElasticClient elasticClient, IElasticIndexSettings elasticIndexSettings)
    {
        _elasticClient = elasticClient;
        _elasticIndexSettings = elasticIndexSettings;
    }

    /// <summary>
    ///     Get audit logs by filter
    /// </summary>
    /// <param name="filter">Filter model</param>
    public async Task<PageResponseDto<AuditLogTransactionDomainModel>> GetAuditLogsByFilterAsync(AuditLogFilterRequestDto filter, CancellationToken cancellationToken)
    {
        var response = await _elasticClient.SearchAsync<AuditLogTransactionDomainModel>(w => Search(w, filter), cancellationToken);
        return new PageResponseDto<AuditLogTransactionDomainModel>(filter.Pagination, response.HitsMetadata?.Total?.Value ?? 0, response.Documents);
    }

    /// <summary>
    ///     Internal method for search data in ELK
    /// </summary>
    private ISearchRequest Search(SearchDescriptor<AuditLogTransactionDomainModel> exp, AuditLogFilterRequestDto filter)
    {
        var query = exp
            .From(filter.Pagination.PageNumber - 1)
            .Size(filter.Pagination.PageSize)
            .Query(w => ApplyFilter(w, filter));

        if (!string.IsNullOrEmpty(filter.Sort.ColumnName))
            query = query.Sort(w => ApplySorting(w, filter));

        return query.Index(_elasticIndexSettings.AuditLog);
    }

    /// <summary>
    ///     Apply filter
    /// </summary>
    private QueryContainer ApplyFilter(QueryContainerDescriptor<AuditLogTransactionDomainModel> q, AuditLogFilterRequestDto filter)
    {
        var container = new QueryContainer();

        if (filter.Filter.Service.HasValue)
            container &= q.Term(t => t.Service, filter.Filter.Service.Value);

        if (filter.Filter.NodeId.HasValue)
            container &= q.Term(t => t.NodeId, filter.Filter.NodeId.Value);

        if (!string.IsNullOrEmpty(filter.Filter.CategoryCode))
            container &= q.Match(t => t.Field(x => x.CategoryCode).Query(filter.Filter.CategoryCode));

        if (filter.Filter.EntityId.HasValue)
            container &= q.Term(t => t.EntityId, filter.Filter.EntityId.Value);

        if (!string.IsNullOrEmpty(filter.Filter.Ip))
            container &= q.Match(t => t.Field(x => x.User.Ip).Query(filter.Filter.Ip));

        if (!string.IsNullOrEmpty(filter.Filter.Login))
            container &= q.Match(t => t.Field(x => x.User.Login).Query(filter.Filter.Login));

        if (filter.Filter.Action.Any())
            container &= q.Terms(t => t.Field(w => w.Action).Terms(filter.Filter.Action));

        return container;
    }

    /// <summary>
    ///     Apply sorting
    /// </summary>
    private IPromise<IList<ISort>> ApplySorting(SortDescriptor<AuditLogTransactionDomainModel> exp, AuditLogFilterRequestDto filter)
    {
        return filter.Sort.SortableType == SortableType.Ascending
            ? exp.Ascending(new Field(filter.Sort.ColumnName))
            : exp.Descending(new Field(filter.Sort.ColumnName));
    }
}