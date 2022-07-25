using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Pagination;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Handlers;
using MediatR;
using Moq;

namespace AuditService.Tests.AuditService.Handlers.Handlers;

public class BlockedPlayersLogRequestHandlerTest
{
    [Fact]
    public async Task Handle_Test_Async()
    {
        // Arrange
        var mediator = new Mock<IMediator>();

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
}
