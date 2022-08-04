using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Tests.Fakes;
using AuditService.Tests.Resources;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

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
    public async Task Check_If_Result_FROM_Elastic_Search_Async()
    {
        //Arrange
        var serviceProvider = FakeServiceProvider.GetServiceProviderForLogHandlers<BlockedPlayersLogDomainModel>(TestResources.BlockedPlayersLogResponse, TestResources.BlockedPlayersLog);
        
        var mediatorService = serviceProvider.GetRequiredService<IMediator>();
        
        var filter =  new LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogResponseDto>();

        //Act 
        var result = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

        //Assert
        NotEmpty(result.List);
    }
}
