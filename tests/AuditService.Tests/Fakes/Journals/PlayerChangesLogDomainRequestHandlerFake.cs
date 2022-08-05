using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Handlers.DomainRequestHandlers;
using AuditService.Setup.AppSettings;

namespace AuditService.Tests.Fakes.Journals;

/// <summary>
/// Fake class PlayerChangesLogDomainRequestHandler for testing
/// </summary>
internal class PlayerChangesLogDomainRequestHandlerFake : PlayerChangesLogDomainRequestHandler
{
    public PlayerChangesLogDomainRequestHandlerFake(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    /// Fake method for testing GetQueryIndex
    /// </summary>
    /// <param name="elasticIndexSettings">IElasticIndexSettings</param>
    /// <returns>Query index</returns>
    internal string? GetQueryIndexFake(IElasticIndexSettings elasticIndexSettings)
    {
        return base.GetQueryIndex(elasticIndexSettings);
    }

    /// <summary>
    /// Fake method for testing GetColumnNameToSort
    /// </summary>
    /// <param name="logSortModel">LogSortDto</param>
    /// <returns>Column name no sort</returns>
    internal string GetColumnNameToSortFake(LogSortDto logSortModel)
    {
        return base.GetColumnNameToSort(logSortModel);
    }
}