using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Tests.Fakes.ServiceData;
using AuditService.Tests.Resources;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;

namespace AuditService.Tests.Tests.Journals.BlockedPlayersLog;

/// <summary>
/// Blocked Players Log Request Handler Test
/// </summary>
public class BlockedPlayersLogRequestHandlerTest
{
    /// <summary>
    /// Unit test for handle method async
    /// </summary>
    [Fact]
    public async Task GetBlockedPlayersLog_CreateBlockedPlayersLog_ResultWithBlockedPlayersLog()
    {
        //Arrange
        var serviceProvider = ServiceProviderFake.GetServiceProviderForLogHandlers<BlockedPlayersLogDomainModel>(TestResources.BlockedPlayersLogResponse, TestResources.BlockedPlayersLog);

        var mediatorService = serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogResponseDto>()
        {
            Filter = new ()
            {
                TimestampFrom = DateTime.Now,
                TimestampTo = DateTime.Now
            }
        };
        
        //Act 
        var result = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

        //Assert
        NotEmpty(result.List);
    }

    /// <summary>
    ///     Validation of blocked players log response
    /// </summary>
    [Fact]
    public async Task BlockedPlayersLogResponseValidation_CreateBlockedPlayersLog_HandlerResponseCorrespondsToTheExpected()
    {
        //Arrange
        var serviceProvider = ServiceProviderFake.GetServiceProviderForLogHandlers<BlockedPlayersLogDomainModel>(TestResources.ElasticSearchResponse, TestResources.DefaultIndex);

        var mediatorService = serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogResponseDto>()
        {
            Filter = new ()
            {
                TimestampFrom = DateTime.Now,
                TimestampTo = DateTime.Now
            }
        };
        
        var expected = JsonConvert.DeserializeObject<List<BlockedPlayersLogDomainModel>>(Encoding.Default.GetString(TestResources.ElasticSearchResponse))
                ?.FirstOrDefault();

        //Act 
        var result = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

        var actual = result.List.FirstOrDefault(x => x.PlayerId == expected.PlayerId);

        //Assert
        Equal(expected.Timestamp, actual.Timestamp);
        Equal(expected.BlockingDate, actual.BlockingDate);
        Equal(expected.PreviousBlockingDate, actual.PreviousBlockingDate);
        Equal(expected.PlayerLogin, actual.PlayerLogin);
        Equal(expected.BlocksCounter, actual.BlocksCounter);
        Equal(expected.PlayerId, actual.PlayerId);
        Equal(expected.Browser, actual.Browser);
        Equal(expected.NodeId, actual.NodeId);
        Equal(expected.Language, actual.Language);
        Equal(expected.Platform, actual.OperatingSystem);
        Equal(expected.LastVisitIpAddress, actual.PlayerIp);
        Equal(expected.BrowserVersion, actual.BrowserVersion);
    }
}
