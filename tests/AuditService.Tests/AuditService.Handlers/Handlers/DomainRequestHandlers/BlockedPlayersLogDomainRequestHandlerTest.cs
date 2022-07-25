using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Extensions;
using AuditService.Handlers.Handlers.DomainRequestHandlers;
using AuditService.Setup.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace AuditService.Tests.AuditService.Handlers.Handlers.DomainRequestHandlers;

public class BlockedPlayersLogDomainRequestHandlerTest : BlockedPlayersLogDomainRequestHandler
{
    private readonly IElasticIndexSettings _elasticIndexSettings;

    public BlockedPlayersLogDomainRequestHandlerTest(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _elasticIndexSettings = serviceProvider.GetRequiredService<IElasticIndexSettings>();
    }

    
    [Fact]
    public void QueryIndex_Test()
    {
        var filter = GetQueryIndex(_elasticIndexSettings);

        Assert.NotNull(filter!);

        Assert.Equal(filter!, _elasticIndexSettings.BlockedPlayersLog!);
    }

    [Theory]
    [MemberData(nameof(ApplyFilterData))]
    public void ApplyFilter_Test(QueryContainerDescriptor<BlockedPlayersLogDomainModel> queryContainerDescriptor, BlockedPlayersLogFilterDto filter, QueryContainer expected )
    {
        var result = ApplyFilter(queryContainerDescriptor, filter);

        Assert.Equal(expected, result);
    }
    
    [Theory]
    [MemberData(nameof(ApplySortingData))]
    public void ApplySorting_Test(SortDescriptor<BlockedPlayersLogDomainModel> sortDescriptor, BlockedPlayersLogSortDto logSortModel, SortDescriptor<BlockedPlayersLogDomainModel> expected)
    {
        var result = ApplySorting(sortDescriptor, logSortModel) as SortDescriptor<BlockedPlayersLogDomainModel>;

        Assert.Equal(expected, result);
    }


    [Theory]
    [MemberData(nameof(GetColumnNameToSortData))]
    public void GetColumnNameToSort_Test(BlockedPlayersLogSortDto actual, string expected)
    {
        var result = GetColumnNameToSort(actual);

        Assert.Equal(expected, result);
    }

    private IEnumerable<object> GetColumnNameToSortData()
    {
        yield return new object[]
        {
            new BlockedPlayersLogSortDto {FieldSortType = BlockedPlayersLogSortType.BlockingDate}, nameof(BlockedPlayersLogDomainModel.BlocksCounter).ToCamelCase()
        };
        yield return new object[]
        {
            new BlockedPlayersLogSortDto {FieldSortType = BlockedPlayersLogSortType.BlocksCounter}, nameof(BlockedPlayersLogDomainModel.BlockingDate).ToCamelCase()
        };
        yield return new object[]
        {
            new BlockedPlayersLogSortDto {FieldSortType = BlockedPlayersLogSortType.PreviousBlockingDate}, nameof(BlockedPlayersLogDomainModel.PreviousBlockingDate).ToCamelCase()
        };
        yield return new object[]
        {
            new BlockedPlayersLogSortDto {FieldSortType = BlockedPlayersLogSortType.Version}, nameof(BlockedPlayersLogDomainModel.BrowserVersion).ToCamelCase()
        };
        yield return new object[]
        {
            new BlockedPlayersLogSortDto(), nameof(BlockedPlayersLogDomainModel.Timestamp).ToCamelCase()
        };
    }

    private IEnumerable<object> ApplySortingData()
    {
        var sortDescriptor = new SortDescriptor<BlockedPlayersLogDomainModel>();
        var logSortModel = new BlockedPlayersLogSortDto() {FieldSortType = BlockedPlayersLogSortType.Version};

        yield return new object[]
        {
            sortDescriptor,
            logSortModel,
            sortDescriptor.Field(field => field.BrowserVersion.UseSuffix(), (SortOrder) logSortModel.SortableType)
        };
        yield return new object[]
        {
            base.ApplySorting(sortDescriptor, logSortModel)
        };
    }
    
    private IEnumerable<object> ApplyFilterData()
    {
        var queryContainerDescriptor = new QueryContainerDescriptor<BlockedPlayersLogDomainModel> ();
        var filter = new BlockedPlayersLogFilterDto();
        var container = new QueryContainer();


        filter.BlockingDateFrom = DateTime.Now.AddMonths(-1);
        container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.BlockingDate).GreaterThan(filter.BlockingDateFrom.Value));
        yield return new object[] {  queryContainerDescriptor, filter, container };

        
        filter.BlockingDateTo = DateTime.Now.AddMonths(1);
        container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.BlockingDate).LessThan(filter.BlockingDateTo.Value));
        yield return new object[] {  queryContainerDescriptor, filter, container };
        
        
        filter.PreviousBlockingDateFrom = DateTime.Now.AddMonths(-2);
        container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.PreviousBlockingDate).GreaterThan(filter.PreviousBlockingDateFrom.Value));
        yield return new object[] {  queryContainerDescriptor, filter, container };
        
        
        filter.PreviousBlockingDateTo = DateTime.Now.AddMonths(-1);
        container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.PreviousBlockingDate).LessThan(filter.PreviousBlockingDateTo.Value));
        yield return new object[] {  queryContainerDescriptor, filter, container };

        
        filter.PlayerLogin = "player@gmail.com";
        container &= queryContainerDescriptor.Match(t => t.Field(x => x.PlayerLogin).Query(filter.PlayerLogin));
        yield return new object[] {  queryContainerDescriptor, filter, container };
        
        
        filter.PlayerId = Guid.NewGuid();
        container &= queryContainerDescriptor.Term(t => t.PlayerId.UseSuffix(), filter.PlayerId.Value);
        yield return new object[] {  queryContainerDescriptor, filter, container };
        
        
        filter.PlayerIp = "0.0.0.0";
        container &= queryContainerDescriptor.Match(t => t.Field(x => x.LastVisitIpAddress).Query(filter.PlayerIp));
        yield return new object[] {  queryContainerDescriptor, filter, container };
        
        
        filter.HallId = Guid.NewGuid();
        container &= queryContainerDescriptor.Term(t => t.HallId.UseSuffix(), filter.HallId.Value);
        yield return new object[] {  queryContainerDescriptor, filter, container };
        
        
        filter.Platform = "windows";
        container &= queryContainerDescriptor.Match(t => t.Field(x => x.Platform).Query(filter.Platform));
        yield return new object[] {  queryContainerDescriptor, filter, container };
        
        
        filter.Browser = "chrome";
        container &= queryContainerDescriptor.Match(t => t.Field(x => x.Browser).Query(filter.Browser));
        yield return new object[] {  queryContainerDescriptor, filter, container };
        
        
        filter.Version = "1";
        container &= queryContainerDescriptor.Match(t => t.Field(x => x.BrowserVersion).Query(filter.Version));
        yield return new object[] {  queryContainerDescriptor, filter, container };
        
        filter.Language = "wn";
        container &= queryContainerDescriptor.Match(t => t.Field(x => x.Language).Query(filter.Language));
        yield return new object[] {  queryContainerDescriptor, filter, container };
    }
}
