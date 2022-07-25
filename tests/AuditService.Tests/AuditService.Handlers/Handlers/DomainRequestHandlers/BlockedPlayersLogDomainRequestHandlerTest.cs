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

    [Fact]
    public void ApplyFilter_Test()
    {
        QueryContainerDescriptor<BlockedPlayersLogDomainModel> queryContainerDescriptor = new QueryContainerDescriptor<BlockedPlayersLogDomainModel>();
        BlockedPlayersLogFilterDto filter = new BlockedPlayersLogFilterDto();
        QueryContainer expected = new QueryContainer();
        
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
    
}