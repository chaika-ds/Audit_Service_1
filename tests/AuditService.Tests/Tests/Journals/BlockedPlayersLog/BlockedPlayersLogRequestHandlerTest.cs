using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Tests.Helpers.Journals;
using AuditService.Tests.Resources;

namespace AuditService.Tests.Tests.Journals.BlockedPlayersLog;

/// <summary>
/// Blocked Players Log Request Handler Test
/// </summary>
public class BlockedPlayersLogRequestHandlerTest
{
    /// <summary>
    ///     Expected blocked players log domain model
    /// </summary>
    private readonly List<BlockedPlayersLogDomainModel> _expectedBlockedPlayersLog;

    public BlockedPlayersLogRequestHandlerTest()
    {
        _expectedBlockedPlayersLog = LogsTestHelper<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogDomainModel, BlockedPlayersLogDomainModel>
            .GetExpectedDomainModels(TestResources.BlockedPlayersLog, TestResources.BlockedPlayersLogResponse);
    }

    /// <summary>
    /// Unit test for handle method async
    /// </summary>
    [Fact]
    public async Task GetBlockedPlayersLog_CreateBlockedPlayersLog_ResultWithBlockedPlayersLog()
    {
        await LogsTestHelper<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogResponseDto, BlockedPlayersLogDomainModel>
                .CheckReturnResult(TestResources.BlockedPlayersLog, TestResources.BlockedPlayersLogResponse);
    }

    /// <summary>
    ///     Validation of blocked players log response
    /// </summary>
    [Fact]
    public async Task BlockedPlayersLogResponseValidation_CreateBlockedPlayersLog_HandlerResponseCorrespondsToTheExpected()
    {
        //Arrange
        var expected = _expectedBlockedPlayersLog
            ?.FirstOrDefault();

        //Act 
        var result = await LogsTestHelper<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogResponseDto, BlockedPlayersLogDomainModel>
            .GetLogHandlerResponse(TestResources.BlockedPlayersLog, TestResources.BlockedPlayersLogResponse);

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

    /// <summary>
    ///     Validation of blocked players log domain response
    /// </summary>
    [Fact]
    public async Task BlockedPlayersLogDomainResponseValidation_CreateBlockedPlayersDomainLog_HandlerResponseCorrespondsToTheExpected()
    {
        //Arrange
        var expected = _expectedBlockedPlayersLog
                ?.FirstOrDefault();

        //Act 
        var result = await LogsTestHelper<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogDomainModel, BlockedPlayersLogDomainModel>
            .GetLogHandlerResponse(TestResources.BlockedPlayersLog, TestResources.BlockedPlayersLogResponse);

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
        Equal(expected.Platform, actual.Platform);
        Equal(expected.LastVisitIpAddress, actual.LastVisitIpAddress);
        Equal(expected.BrowserVersion, actual.BrowserVersion);
    }
}
