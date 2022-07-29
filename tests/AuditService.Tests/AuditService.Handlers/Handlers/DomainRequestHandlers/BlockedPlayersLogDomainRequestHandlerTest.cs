using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Consts;
using AuditService.Handlers.Handlers.DomainRequestHandlers;
using AuditService.Setup.AppSettings;
using AuditService.Tests.AuditService.GetAuditLog.Models;
using AuditService.Tests.Factories.Fakes;
using AuditService.Tests.Resources;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace AuditService.Tests.AuditService.Handlers.Handlers.DomainRequestHandlers;

/// <summary>
/// Blocked Players Log Domain Request Handler Test
/// </summary>
public class BlockedPlayersLogDomainRequestHandlerTest : BlockedPlayersLogDomainRequestHandler
{
    private static readonly IServiceProvider ServiceProvider = GetServiceProvider();
    private readonly IElasticIndexSettings _elasticIndexSettings;

    /// <summary>
    /// Initialize Blocked Players Log Domain Request Handler
    /// </summary>
    public BlockedPlayersLogDomainRequestHandlerTest() : base(ServiceProvider)
    {
        _elasticIndexSettings = ServiceProvider.GetRequiredService<IElasticIndexSettings>();
    }

    /// <summary>
    /// Unit Test for GetQueryIndex
    /// </summary>
    [Fact]
    public void Check_QueryIndex_Method_Return_IndexName()
    {
        var filter = GetQueryIndex(_elasticIndexSettings);

        Assert.Equal(filter!, _elasticIndexSettings.BlockedPlayersLog!);
    }

    /// <summary>
    /// Unit Test for ApplyFilter
    /// </summary>
    [Theory]
    [MemberData(nameof(ApplyFilterData))]
    public void Check_ApplyFilter_Method_Return_Result(QueryContainerDescriptor<BlockedPlayersLogDomainModel> queryContainerDescriptor, BlockedPlayersLogFilterDto filter, IQueryContainer expected)
    {
        var result = ApplyFilter(queryContainerDescriptor, filter);

        if (expected is QueryContainerDescriptor<BlockedPlayersLogDomainModel>)
            Assert.Contains(expected, (result as IQueryContainer).Bool.Must);
        else
            Assert.Equal(expected, result);

        Assert.True(true);
    }

    /// <summary>
    /// Unit Test for ApplySorting
    /// </summary>
    [Theory]
    [MemberData(nameof(ApplySortingData))]
    public void Check_ApplySorting_Method_Return_Sort_Field(SortDescriptor<BlockedPlayersLogDomainModel> sortDescriptor, BlockedPlayersLogSortDto logSortModel,
        SortDescriptor<BlockedPlayersLogDomainModel> expected)
    {
        var result = ApplySorting(sortDescriptor, logSortModel) as SortDescriptor<BlockedPlayersLogDomainModel>;

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Unit Test for GetColumnNameToSort
    /// </summary>
    [Theory]
    [MemberData(nameof(GetColumnNameToSortData))]
    public void Check_GetColumnNameToSort_Return_SortedColumName(BlockedPlayersLogSortDto actual, string expected)
    {
        var result = GetColumnNameToSort(actual);

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Input test data for GetColumnNameToSort
    /// </summary>
    private static IEnumerable<object> GetColumnNameToSortData()
    {
        yield return new object[]
        {
            new BlockedPlayersLogSortDto {FieldSortType = BlockedPlayersLogSortType.BlockingDate}, nameof(BlockedPlayersLogDomainModel.BlockingDate).ToCamelCase()
        };
        yield return new object[]
        {
            new BlockedPlayersLogSortDto {FieldSortType = BlockedPlayersLogSortType.BlocksCounter}, nameof(BlockedPlayersLogDomainModel.BlocksCounter).ToCamelCase()
        };
        yield return new object[]
        {
            new BlockedPlayersLogSortDto {FieldSortType = BlockedPlayersLogSortType.PreviousBlockingDate}, nameof(BlockedPlayersLogDomainModel.PreviousBlockingDate).ToCamelCase()
        };
        yield return new object[]
        {
            new BlockedPlayersLogSortDto {FieldSortType = BlockedPlayersLogSortType.Version}, nameof(BlockedPlayersLogDomainModel.BrowserVersion).ToCamelCase()
        };
    }

    /// <summary>
    /// Input test data for ApplySorting
    /// </summary>
    private static IEnumerable<object> ApplySortingData()
    {
        var sortDescriptor = new SortDescriptor<BlockedPlayersLogDomainModel>();
        var logSortModel = new BlockedPlayersLogSortDto() {FieldSortType = BlockedPlayersLogSortType.Version};

        yield return new object[]
        {
            sortDescriptor,
            logSortModel,
            sortDescriptor.Field(field => field.BrowserVersion.Suffix(ElasticConst.SuffixKeyword), (SortOrder) logSortModel.SortableType)
        };
    }

    /// <summary>
    /// Input test data for ApplyFilter
    /// </summary>
    private static IEnumerable<object> ApplyFilterData()
    {
        var queryContainerDescriptor = new QueryContainerDescriptor<BlockedPlayersLogDomainModel>();
        var filter = new BlockedPlayersLogFilterDto();
        var container = new QueryContainer();


        filter.BlockingDateFrom = DateTime.Now.AddMonths(-1);
        container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.BlockingDate).GreaterThan(filter.BlockingDateFrom.Value));
        yield return new object[] {queryContainerDescriptor, filter, container};


        filter.BlockingDateTo = DateTime.Now.AddMonths(1);
        container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.BlockingDate).LessThan(filter.BlockingDateTo.Value));
        yield return new object[] {queryContainerDescriptor, filter, container};


        filter.PreviousBlockingDateFrom = DateTime.Now.AddMonths(-2);
        container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.PreviousBlockingDate).GreaterThan(filter.PreviousBlockingDateFrom.Value));
        yield return new object[] {queryContainerDescriptor, filter, container};


        filter.PreviousBlockingDateTo = DateTime.Now.AddMonths(-1);
        container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.PreviousBlockingDate).LessThan(filter.PreviousBlockingDateTo.Value));
        yield return new object[] {queryContainerDescriptor, filter, container};


        filter.PlayerLogin = "player@gmail.com";
        container &= queryContainerDescriptor.Match(t => t.Field(x => x.PlayerLogin).Query(filter.PlayerLogin));
        yield return new object[] {queryContainerDescriptor, filter, container};


        filter.PlayerId = Guid.NewGuid();
        container &= queryContainerDescriptor.Term(t => t.PlayerId.Suffix(ElasticConst.SuffixKeyword), filter.PlayerId.Value);
        yield return new object[] {queryContainerDescriptor, filter, container};


        filter.PlayerIp = "0.0.0.0";
        container &= queryContainerDescriptor.Match(t => t.Field(x => x.LastVisitIpAddress).Query(filter.PlayerIp));
        yield return new object[] {queryContainerDescriptor, filter, container};


        filter.HallId = Guid.NewGuid();
        container &= queryContainerDescriptor.Term(t => t.HallId.Suffix(ElasticConst.SuffixKeyword), filter.HallId.Value);
        yield return new object[] {queryContainerDescriptor, filter, container};


        filter.Platform = "windows";
        container &= queryContainerDescriptor.Match(t => t.Field(x => x.Platform).Query(filter.Platform));
        yield return new object[] {queryContainerDescriptor, filter, container};


        filter.Browser = "chrome";
        container &= queryContainerDescriptor.Match(t => t.Field(x => x.Browser).Query(filter.Browser));
        yield return new object[] {queryContainerDescriptor, filter, container};


        filter.Version = "1";
        container &= queryContainerDescriptor.Match(t => t.Field(x => x.BrowserVersion).Query(filter.Version));
        yield return new object[] {queryContainerDescriptor, filter, container};

        filter.Language = "wn";
        container &= queryContainerDescriptor.Match(t => t.Field(x => x.Language).Query(filter.Language));
        yield return new object[] {queryContainerDescriptor, filter, container};
    }

    /// <summary>
    ///     Getting fake service provider 
    /// </summary>
    private static IServiceProvider GetServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddScoped<IElasticIndexSettings, FakeElasticSearchSettings>();
        services.AddScoped(_ => FakeElasticSearchClientProvider.GetFakeElasticSearchClient<BlockedPlayersLogDomainModel>(TestResources.ElasticSearchResponse));

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}