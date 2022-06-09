using AuditService.Data.Domain.Domain;
using AuditService.Data.Domain.Dto;
using AuditService.Data.Domain.Dto.Filter;
using AuditService.Data.Domain.Enums;
using AuditService.WebApiApp.AppSettings;
using AuditService.WebApiApp.Services.Interfaces;
using Nest;

namespace AuditService.WebApiApp.Services;

/// <summary>
///     Audit log provider
/// </summary>
public class AuditLogService : IAuditLogService
{
    private readonly IElasticClient _elasticClient;
    private readonly IElasticIndex _elasticIndex;

    public AuditLogService(IElasticClient elasticClient, IElasticIndex elasticIndex)
    {
        _elasticClient = elasticClient;
        _elasticIndex = elasticIndex;
    }

    /// <summary>
    ///     Get audit logs by filter
    /// </summary>
    /// <param name="filter">Filter model</param>
    public async Task<PageResponseDto<AuditLogTransactionDomainModel>> GetLogsByFilterAsync(AuditLogFilterRequestDto filter)
    {
        var response = await _elasticClient.SearchAsync<AuditLogTransactionDomainModel>(w => Search(w, filter));
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

        return query.Index(_elasticIndex.AuditLog);
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