using AuditService.Common.Enums;
using AuditService.Common.Helpers;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Handlers;
using AuditService.Tests.AuditService.Handlers.Asserts;
using AuditService.Tests.AuditService.Handlers.Fakes;
using AuditService.Tests.AuditService.Handlers.Mock;
using AuditService.Tests.Extensions;
using static Xunit.Assert;

namespace AuditService.Tests.AuditService.Handlers;

/// <summary>
/// LogRequestBaseHandler test
/// </summary>
public class LogRequestBaseHandlerTest
{
    private readonly HandlerMock<PlayerChangesLogFilterDto, LogSortDto,
        PlayerChangesLogDomainModel> _handlersMock;

    private readonly CancellationToken _tokenTest;


    public LogRequestBaseHandlerTest()
    {
        _handlersMock = new HandlerMock<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogDomainModel>();
        var cts = new CancellationTokenSource();
        _tokenTest = cts.Token;
    }

    /// <summary>
    /// Testing Handle Method
    /// </summary>
    [Fact]
    public async Task LogRequestBaseHandler_HandlerWorks_AllDataIsHandled()
    {
        //Arrange
        var handleSendResponse = LogRequestBaseHandlerResponsesFake.GetSendHandleResponse();
        var responseModelsSendResponse = LogRequestBaseHandlerResponsesFake.GetSendResponseModelsResponse();
        var tryLocalizeResponse = LogRequestBaseHandlerResponsesFake.GetTestTryLocalizeResponse();

        var handlerTest = new PlayerChangesLogRequestHandler(
            _handlersMock.MediatorMock(handleSendResponse, responseModelsSendResponse),
            _handlersMock.LocalizerMock(tryLocalizeResponse));

        var logFilterRequest = LogRequestBaseHandlerTestRequests.GetTestLogFilterRequest();

        //Act
        var playerChangesLogHandleResponse = await handlerTest.Handle(logFilterRequest, _tokenTest);

        //Assert
        IsPlayerChangesLogReceived(playerChangesLogHandleResponse);
        LogRequestBaseHandlerAsserts.IsEqualPaginationResponse(playerChangesLogHandleResponse.Pagination, handleSendResponse.Pagination);
        LogRequestBaseHandlerAsserts.IsEqualPlayerChangesLogResponse(playerChangesLogHandleResponse.List.ToList(), handleSendResponse.List.ToList(),
            responseModelsSendResponse.Values.SelectMany(v => v.Select(x => x.Name)).ToArray());
        LogRequestBaseHandlerAsserts.IsLocalizePlayerAttributeLocalized(playerChangesLogHandleResponse.List.ToList(),
            handleSendResponse.List.ToList(), tryLocalizeResponse);
    }

    #region Testing GenerateResponseModelsAsync

    /// <summary>
    /// Testing GenerateResponseModelsAsync Method
    /// </summary>
    [Fact]
    public async Task LogRequestBaseHandler_GenerateResponseModels_GetPlayerChangesLogResponseDto()
    {
        //Arrange
        var handlerTest = new PlayerChangesLogRequestHandler(
            _handlersMock.MediatorMock(LogRequestBaseHandlerResponsesFake.GetSendHandleResponse(),
                LogRequestBaseHandlerResponsesFake.GetSendResponseModelsResponse()),
            _handlersMock.LocalizerMock(LogRequestBaseHandlerResponsesFake.GetTestTryLocalizeResponse()));

        IEnumerable<PlayerChangesLogDomainModel> domainModels =
            LogRequestBaseHandlerResponsesFake.GetTestPlayerChangesLogDomainModel();
        var cts = new CancellationTokenSource();
        var playerChangesLogResponseDto = LogRequestBaseHandlerTestRequests.GetPlayerChangesLogResponseDtoTestRequest();

        //Act
        var responseInvoke =
            await handlerTest.Invoke<Task<IEnumerable<PlayerChangesLogResponseDto>>>("GenerateResponseModelsAsync",
                domainModels, FakeValues.LanguageTest, _tokenTest);

        ////Assert
        Equal(playerChangesLogResponseDto.SerializeToString(), responseInvoke.ToList().SerializeToString());
    }

    /// <summary>
    /// Testing GenerateResponseModelsAsync Method - not all Key
    /// </summary>
    [Fact]
    public async Task LogRequestBaseHandler_GenerateResponseModels_GetNoLocalizedKey()
    {
        //Arrange
        var handlerTest = new PlayerChangesLogRequestHandler(
            _handlersMock.MediatorMock(null!, LogRequestBaseHandlerResponsesFake.GetSendResponseModelsResponse()),
            _handlersMock.LocalizerMock(LogRequestBaseHandlerResponsesFake.GetTestTryLocalizeResponseNotAllKey()));
        IEnumerable<PlayerChangesLogDomainModel> domainModels =
            LogRequestBaseHandlerResponsesFake.GetTestPlayerChangesLogDomainModelNullResponse();

        //Act
        var responseInvoke =
            await handlerTest.Invoke<Task<IEnumerable<PlayerChangesLogResponseDto>>>("GenerateResponseModelsAsync",
                domainModels, FakeValues.LanguageTest, _tokenTest);

        //Assert
        IsNotType<IEnumerable<PlayerChangesLogResponseDto>>(() => responseInvoke);
    }

    /// <summary>
    /// Testing GenerateResponseModelsAsync Method Throw Exception
    /// </summary>
    [Fact]
    public void LogRequestBaseHandler_GenerateResponseModelsAsyncThrowException_KeyNotFoundException()
    {
        //Arrange
        var handlerTest = new PlayerChangesLogRequestHandler(
            _handlersMock.MediatorMock(LogRequestBaseHandlerResponsesFake.GetSendHandleResponse(),
                LogRequestBaseHandlerResponsesFake.GetSendNoConcidenceKeyResponse()),
            _handlersMock.LocalizerMock(LogRequestBaseHandlerResponsesFake.GetTestTryLocalizeResponse()));

        IEnumerable<PlayerChangesLogDomainModel> domainModels =
            LogRequestBaseHandlerResponsesFake.GetTestPlayerChangesLogDomainModel();

        //Act
        var responseInvoke =
            handlerTest.Invoke<Task<IEnumerable<PlayerChangesLogResponseDto>>>("GenerateResponseModelsAsync",
                domainModels, FakeValues.LanguageTest, _tokenTest);

        //Assert
        NotNull(() => responseInvoke.Exception!);
    }

    #endregion

    #region Testing GenerateResponseModelsAsync with grouped data 

    /// <summary>
    /// Testing GenerateResponseGroupedModelsAsync Method with grouped data 
    /// </summary>
    [Fact]
    public async Task LogRequestBaseHandler_GenerateResponseGroupedModels_GetPlayerChangesLogResponseDto()
    {
        //Arrange
        var handlerTest = new PlayerChangesLogRequestHandler(
            _handlersMock.MediatorMock(LogRequestBaseHandlerResponsesFake.GetSendHandleResponse(),
                LogRequestBaseHandlerResponsesFake.GetSendResponseModelsResponse()),
            _handlersMock.LocalizerMock(LogRequestBaseHandlerResponsesFake.GetTestTryLocalizeResponse()));

        IGrouping<ModuleName, PlayerChangesLogDomainModel> groupedModels = LogRequestBaseHandlerResponsesFake
            .GetTestPlayerChangesLogDomainModel().GroupBy(model => model.ModuleName).First();
        EventDomainModel[] events = LogRequestBaseHandlerResponsesFake.GetTestEventDomainModelArray();

        var playerChangesLogResponseDto =
            LogRequestBaseHandlerTestRequests.GetPlayerChangesLogResponseGroupedDtoTestRequest();

        //Act
        var responseInvoke =
            await handlerTest.Invoke<Task<IEnumerable<PlayerChangesLogResponseDto>>>(
                "GenerateResponseModelsAsync", groupedModels, events, FakeValues.LanguageTest, _tokenTest);

        //Assert
        Equal(playerChangesLogResponseDto.SerializeToString(), responseInvoke.ToList().SerializeToString());
    }

    /// <summary>
    /// Testing GenerateResponseModelsAsync Method with grouped data - not all Key
    /// </summary>
    [Fact]
    public async Task LogRequestBaseHandler_GenerateGroupedResponseModels_GetNoLocalizedKey()
    {
        //Arrange
        var handlerTest = new PlayerChangesLogRequestHandler(
            _handlersMock.MediatorMock(null!, LogRequestBaseHandlerResponsesFake.GetSendResponseModelsResponse()),
            _handlersMock.LocalizerMock(LogRequestBaseHandlerResponsesFake.GetTestTryLocalizeResponseNotAllKey()));

        IGrouping<ModuleName, PlayerChangesLogDomainModel> groupedModels = LogRequestBaseHandlerResponsesFake
            .GetTestPlayerChangesLogDomainModel().GroupBy(model => model.ModuleName).First();
        var events = LogRequestBaseHandlerResponsesFake.GetTestEventDomainModelArray();

        //Act
        var responseInvoke =
            await handlerTest.Invoke<Task<IEnumerable<PlayerChangesLogResponseDto>>>(
                "GenerateResponseModelsAsync", groupedModels, events, FakeValues.LanguageTest, _tokenTest);

        //Assert
        IsNotType<IEnumerable<PlayerChangesLogResponseDto>>(() => responseInvoke);
    }

    /// <summary>
    /// Testing GenerateResponseGroupedModelsAsync Method with grouped data Throw Exception
    /// </summary>
    [Fact]
    public void LogRequestBaseHandler_GenerateGroupedResponseModelsAsyncThrowException_KeyNotFoundException()
    {
        //Arrange
        var handlerTest = new PlayerChangesLogRequestHandler(
            _handlersMock.MediatorMock(LogRequestBaseHandlerResponsesFake.GetSendHandleResponse(),
                LogRequestBaseHandlerResponsesFake.GetSendNoConcidenceKeyResponse()),
            _handlersMock.LocalizerMock(LogRequestBaseHandlerResponsesFake.GetTestTryLocalizeResponse()));

        IGrouping<ModuleName, PlayerChangesLogDomainModel> groupedModels = LogRequestBaseHandlerResponsesFake
            .GetTestPlayerChangesLogDomainModel().GroupBy(model => model.ModuleName).First();
        var events = LogRequestBaseHandlerResponsesFake.GetTestEventDomainModelArray();

        //Act
        var responseInvoke =
            handlerTest.Invoke<Task<IEnumerable<PlayerChangesLogResponseDto>>>("GenerateResponseModelsAsync",
                groupedModels, events, FakeValues.LanguageTest, _tokenTest);

        //Assert
        NotNull(() => responseInvoke.Exception!);
    }

    #endregion

    #region Testing mocked method returns Exception

    /// <summary>
    /// Testing when ELK Send method with LogFilterRequestDto params returns Exception
    /// </summary>
    [Fact]
    public void LogRequestBaseHandler_ELKSendLogFilterRequestDtoException_ExceptionHandled()
    {
        //Arrange
        var handlerTest = new PlayerChangesLogRequestHandler(_handlersMock.MediatorLogFilterRequestExceptionMock(),
            _handlersMock.LocalizerEmptyMock());

        //Act
        var playerChangesLogHandleResponse =
            handlerTest.Handle(LogRequestBaseHandlerTestRequests.GetTestLogFilterRequest(), _tokenTest);

        //Assert
        ThrowsAnyAsync<Exception>(() => playerChangesLogHandleResponse);
    }

    /// <summary>
    /// Testing when ELK Send method with EventsRequest params returns Exception
    /// </summary>
    [Fact]
    public void LogRequestBaseHandler_ELKSendEventsRequestException_ExceptionHandled()
    {
        //Arrange
        var handlerTest = new PlayerChangesLogRequestHandler(_handlersMock.MediatorEventsRequestExceptionMock(),
            _handlersMock.LocalizerEmptyMock());

        //Act
        var playerChangesLogHandleResponse =
            handlerTest.Handle(LogRequestBaseHandlerTestRequests.GetTestLogFilterRequest(), _tokenTest);

        //Assert
        ThrowsAnyAsync<Exception>(() => playerChangesLogHandleResponse);
    }

    /// <summary>
    /// Testing when ELK Send method with EventsRequest params returns Exception
    /// </summary>
    [Fact]
    public void LogRequestBaseHandler_LocalizerException_ExceptionHandled()
    {
        //Arrange
        var handlerTest = new PlayerChangesLogRequestHandler(_handlersMock.MediatorEmptyMock(),
            _handlersMock.LocalizerExceptionMock());

        //Act
        var playerChangesLogHandleResponse =
            handlerTest.Handle(LogRequestBaseHandlerTestRequests.GetTestLogFilterRequest(), _tokenTest);

        //Assert
        ThrowsAnyAsync<Exception>(() => playerChangesLogHandleResponse);
    }

    #endregion
}