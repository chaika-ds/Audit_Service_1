using AuditService.Common.Enums;
using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Filter.VisitLog;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Common.Models.Dto.VisitLog;
using AuditService.Tests.Helpers.Journals;
using AuditService.Tests.Resources;

namespace AuditService.Tests.Tests.Journals.VisitLog;

/// <summary>
///     Test of PlayerVisitLogDomainRequestHandler
/// </summary> 
public class VisitLogTest
{
    /// <summary>
    ///     Expected user visit log
    /// </summary>
    private readonly List<UserVisitLogDomainModel> _expectedUserVisitLog;

    /// <summary>
    ///     Expected player visit log
    /// </summary>
    private readonly List<PlayerVisitLogDomainModel> _expectedPlayerVisitLog;

    public VisitLogTest()
    {
        _expectedUserVisitLog = LogsTestHelper<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogDomainModel, UserVisitLogDomainModel>
            .GetExpectedDomainModels(TestResources.VisitLog, TestResources.ElasticSearchVisitLogResponse);

        _expectedPlayerVisitLog = LogsTestHelper<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel, PlayerVisitLogDomainModel>
            .GetExpectedDomainModels(TestResources.VisitLog, TestResources.ElasticSearchVisitLogResponse);
    }

    /// <summary>
    ///     Check if the result is coming from players visit log domain handler
    /// </summary>
    [Fact]
    public async Task GetPlayerVisitLogs_CreateVisitLog_ResultWithPlayerDomainLogs()
    {
        await LogsTestHelper<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel, PlayerVisitLogDomainModel>
            .CheckReturnResult(TestResources.VisitLog, TestResources.ElasticSearchVisitLogResponse);
    }

    /// <summary>
    ///     Check if the result is coming from players visit log handler
    /// </summary>
    [Fact]
    public async Task GetPlayerVisitLogs_CreateVisitLog_ResultWithPlayerDroLogs()
    {
        await LogsTestHelper<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogResponseDto, PlayerVisitLogDomainModel>
            .CheckReturnResult(TestResources.VisitLog, TestResources.ElasticSearchVisitLogResponse);
    }

    /// <summary>
    ///     Validation of player visit log response
    /// </summary>
    [Fact]
    public async Task PlayerVisitLogResponseValidation_CreateVisitLog_HandlerResponseCorrespondsToTheExpected()
    {
        //Arrange      
        var expected = _expectedPlayerVisitLog
           ?.FirstOrDefault(x => x.Type == VisitLogType.Player.ToString());

        //Act 
        var result = await LogsTestHelper<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel, PlayerVisitLogDomainModel>
            .GetLogHandlerResponse(TestResources.VisitLog, TestResources.ElasticSearchVisitLogResponse);

        var actual = result.List.FirstOrDefault(x => x.PlayerId == expected.PlayerId);

        //Assert
        Equal(expected.Authorization.OperatingSystem, actual.Authorization.OperatingSystem);
        Equal(expected.Authorization.Browser, actual.Authorization.Browser);
        Equal(expected.Authorization.DeviceType, actual.Authorization.DeviceType);
        Equal(expected.Ip, actual.Ip);
        Equal(expected.Login, actual.Login);
        Equal(expected.Timestamp, actual.Timestamp);
        Equal(expected.PlayerId, actual.PlayerId);
        Equal(expected.NodeId, actual.NodeId);
        Equal(expected.Authorization.AuthorizationType!, actual.Authorization.AuthorizationType!);
    }


    /// <summary>
    ///     Check if the result is coming from users visit log domain handler
    /// </summary>
    [Fact]
    public async Task GetUserVisitLogs_CreateVisitLog_ResultWithUserDomainLogs()
    {
        await LogsTestHelper<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogDomainModel, UserVisitLogDomainModel>
            .CheckReturnResult(TestResources.VisitLog, TestResources.ElasticSearchVisitLogResponse);
    }

    /// <summary>
    ///     Validation of user visit log response
    /// </summary>
    [Fact]
    public async Task UserVisitLogResponseValidation_CreateVisitLog_HandlerResponseCorrespondsToTheExpected()
    {
        //Arrange
        var expected = _expectedUserVisitLog
           ?.FirstOrDefault(x => x.Type == VisitLogType.User.ToString());

        //Act 
        var result = await LogsTestHelper<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogDomainModel, UserVisitLogDomainModel>
            .GetLogHandlerResponse(TestResources.VisitLog, TestResources.ElasticSearchVisitLogResponse);

        var actual = result.List.FirstOrDefault(x => x.UserId == expected.UserId);

        //Assert
        Equal(expected.Authorization.OperatingSystem, actual.Authorization.OperatingSystem);
        Equal(expected.Authorization.Browser, actual.Authorization.Browser);
        Equal(expected.Authorization.DeviceType, actual.Authorization.DeviceType);
        Equal(expected.Ip, actual.Ip);
        Equal(expected.Login, actual.Login);
        Equal(expected.Timestamp, actual.Timestamp);
        Equal(expected.NodeId, actual.NodeId);
        Equal(expected.UserId, actual.UserId);
        if (expected.UserRoles != null && expected.UserRoles.Any())
        {
            Equal(expected.UserRoles.FirstOrDefault().Name, actual.UserRoles.FirstOrDefault().Name);
            Equal(expected.UserRoles.FirstOrDefault().Code, actual.UserRoles.FirstOrDefault().Code);
        }
    }

    /// <summary>
    ///     Check if the result is coming from users visit log handler
    /// </summary>
    [Fact]
    public async Task GetUserVisitLogs_CreateVisitLog_ResultWithUserDtoLogs()
    {
        await LogsTestHelper<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogResponseDto, UserVisitLogDomainModel>
            .CheckReturnResult(TestResources.VisitLog, TestResources.ElasticSearchVisitLogResponse);
    }

    /// <summary>
    ///     Check mapping user visit log from domain model to dto
    /// </summary>
    [Fact]
    public async Task CheckMappingUserVisitLogFromDomainModelToDto_CreateVisitLog_ResultWithUserDtoLogs()
    {
        //Arrange
        var resultForDomainModelHandler = await LogsTestHelper<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogDomainModel, UserVisitLogDomainModel>
            .GetLogHandlerResponse(TestResources.VisitLog, TestResources.ElasticSearchVisitLogResponse);

        var expected = resultForDomainModelHandler.List
            ?.FirstOrDefault(x => x.Type == VisitLogType.User.ToString());

        //Act 
        var result = await LogsTestHelper<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogResponseDto, UserVisitLogDomainModel>
            .GetLogHandlerResponse(TestResources.VisitLog, TestResources.ElasticSearchVisitLogResponse);

        var actual = result.List?.FirstOrDefault(x => x.UserId == expected.UserId);

        //Assert
        Equal(expected.Authorization.OperatingSystem, actual.OperatingSystem);
        Equal(expected.Authorization.Browser, actual.Browser);
        Equal(expected.Authorization.DeviceType, actual.DeviceType);
        Equal(expected.Ip, actual.Ip);
        Equal(expected.Login, actual.Login);
        Equal(expected.Timestamp, actual.VisitTime);
        Equal(expected.NodeId, actual.NodeId);
        Equal(expected.UserId, actual.UserId);
        if (expected.UserRoles != null && expected.UserRoles.Any())
        {
            Equal(expected.UserRoles.FirstOrDefault().Name, actual.UserRoles.FirstOrDefault().Name);
            Equal(expected.UserRoles.FirstOrDefault().Code, actual.UserRoles.FirstOrDefault().Code);
        }
    }

    /// <summary>
    ///     Check mapping player visit log from domain model to dto
    /// </summary>
    [Fact]
    public async Task CheckMappingPlayerVisitLogFromDomainModelToDto_CreateVisitLog_DtoMatchesDomainModel()
    {
        //Arrange     
        var resultForDomainModelHandler = await LogsTestHelper<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel, PlayerVisitLogDomainModel>
            .GetLogHandlerResponse(TestResources.VisitLog, TestResources.ElasticSearchVisitLogResponse);

        var expected = resultForDomainModelHandler.List
            ?.FirstOrDefault(x => x.Type == VisitLogType.Player.ToString());

        var filter = new LogFilterRequestDto<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogResponseDto>()
        {
            Filter = new ()
            {
                TimestampFrom = DateTime.Now,
                TimestampTo = DateTime.Now
            }
        };
        //Act 
        var result = await LogsTestHelper<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogResponseDto, PlayerVisitLogDomainModel>
            .GetLogHandlerResponse(TestResources.VisitLog, TestResources.ElasticSearchVisitLogResponse);

        var actual = result.List?.FirstOrDefault(x => x.PlayerId == expected.PlayerId);

        //Assert
        Equal(expected.Authorization.OperatingSystem, actual.OperatingSystem);
        Equal(expected.Authorization.Browser, actual.Browser);
        Equal(expected.Authorization.DeviceType, actual.DeviceType);
        Equal(expected.Ip, actual.Ip);
        Equal(expected.Login, actual.Login);
        Equal(expected.Timestamp, actual.VisitTime);
        Equal(expected.PlayerId, actual.PlayerId);
        Equal(expected.NodeId, actual.NodeId);
        Equal(expected.Authorization.AuthorizationType!, actual.AuthorizationMethod);
    }
}
