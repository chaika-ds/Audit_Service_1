using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Pagination;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers;
using AuditService.Handlers.Handlers;
using AuditService.Setup.AppSettings;
using AuditService.Tests.AuditService.GetAuditLog.Models;
using AuditService.Tests.Factories.Fakes;
using AuditService.Tests.Resources;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tolar.Redis;

namespace AuditService.Tests.AuditService.Handlers.Handlers;


/// <summary>
/// Blocked Players Log Request Handler Test
/// </summary>
public class BlockedPlayersLogRequestHandlerTest
{
    
    /// <summary>
    /// Unit test for handle method async
    /// </summary>
    [Fact]
    public async Task Handle_Test_Async()
    {
        // Arrange
        var mediator = new Mock<IMediator>();

        var mock = await CreateMockDataAsync();

        mediator.Setup(x => x.Send(
                It.IsAny<LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogDomainModel>>(),
                CancellationToken.None))
            .Returns(Task.FromResult(mock));


        var handler = new BlockedPlayersLogRequestHandler(mediator.Object);

        // Act
        var response = await handler.Handle(new LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogResponseDto>()
            , CancellationToken.None);

        // Assert
        Assert.IsType<PageResponseDto<BlockedPlayersLogResponseDto>>(response);

        Assert.NotNull(response);
        Assert.Equal(response.Pagination.Total, mock.Pagination.Total);
        Assert.Equal(response.Pagination.PageNumber, mock.Pagination.PageNumber);
        Assert.Equal(response.Pagination.PageSize, mock.Pagination.PageSize);
        
        Assert.Equal(response.List.FirstOrDefault()?.PlayerId, mock.List.FirstOrDefault()?.PlayerId);
        Assert.Equal(response.List.FirstOrDefault()?.HallId, mock.List.FirstOrDefault()?.HallId);

    }
    
    /// <summary>
    /// Unit test for handle method async
    /// </summary>
    [Fact]
    public async Task Check_If_Result_FROM_Elastic_Search_Async()
    {
        var serviceProvider = GetServiceProvider();
        
        var mediatorService = serviceProvider.GetRequiredService<IMediator>();
        
        var filter =  new LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogResponseDto>();

        //Act 
        var result = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

        //Assert
        Assert.True(result.List.Any());
    }

    private async Task<PageResponseDto<BlockedPlayersLogDomainModel>> CreateMockDataAsync()
    {
        var mock = new PageResponseDto<BlockedPlayersLogDomainModel>(
            new PaginationResponseDto(1, 2, 3),
            new List<BlockedPlayersLogDomainModel>()
            {
                new()
                {
                    ProjectId = Guid.NewGuid(),
                    PlayerId = Guid.NewGuid(),
                    HallId = Guid.NewGuid(),
                }
            });
        
        return await Task.FromResult(mock);
    }
    
    /// <summary>
    ///     Getting fake service provider 
    /// </summary>
    private static IServiceProvider GetServiceProvider()
    {
        var services = new ServiceCollection();

        DiConfigure.RegisterServices(services);
        services.AddSingleton<IRedisRepository, FakeRedisReposetoryForCachePipelineBehavior>();
        services.AddScoped<IElasticIndexSettings, FakeElasticSearchSettings>();
        services.AddLogging();
        services.AddScoped(_ => FakeElasticSearchClientProvider.GetFakeElasticSearchClient<BlockedPlayersLogDomainModel>(TestResources.BlockedPlayersLogResponse, TestResources.BlockedPlayersLog));

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}
