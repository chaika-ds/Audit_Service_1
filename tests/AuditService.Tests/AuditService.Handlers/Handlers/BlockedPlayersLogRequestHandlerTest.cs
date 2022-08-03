using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers;
using AuditService.Setup.AppSettings;
using AuditService.Tests.AuditService.GetAuditLog.Models;
using AuditService.Tests.Factories.Fakes;
using AuditService.Tests.Resources;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
    public async Task Check_If_Result_FROM_Elastic_Search_Async()
    {
        //Arrange
        IServiceProvider serviceProvider = FakeServiceProvider.CreateElkServiceProviderFake<BlockedPlayersLogDomainModel>(TestResources.BlockedPlayersLogResponse);
        
        var mediatorService = serviceProvider.GetRequiredService<IMediator>();
        
        var filter =  new LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogResponseDto>();

        //Act 
        var result = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

        //Assert
        NotEmpty(result.List);
    }
}
