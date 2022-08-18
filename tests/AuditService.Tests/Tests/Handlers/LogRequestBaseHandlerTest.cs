using AuditService.Common.Contexts;
using AuditService.Common.Enums;
using AuditService.Common.Helpers;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Handlers;
using AuditService.Tests.Extensions;
using AuditService.Tests.Fakes.Handlers;
using AuditService.Tests.Mocks;
using AuditService.Tests.TRASH;

namespace AuditService.Tests.Tests.Handlers;

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
    public async Task LogRequestBaseHandler_HandlerWorks_AllDataIsHandledAsync()
    {
        //Arrange
        var handleSendResponse = LogRequestBaseHandlerResponsesFake.GetSendHandleResponse();
        var responseModelsSendResponse = LogRequestBaseHandlerResponsesFake.GetSendResponseModelsResponse();
        var tryLocalizeResponse = LogRequestBaseHandlerResponsesFake.GetTestTryLocalizeResponse();

        // todo переделать!!
        var handlerTest = new PlayerChangesLogRequestHandler(
            _handlersMock.MediatorMock(handleSendResponse, responseModelsSendResponse),
            _handlersMock.LocalizerMock(tryLocalizeResponse), new RequestContext());

        var logFilterRequest = LogRequestBaseHandlerTestRequests.GetTestLogFilterRequest();

        //Act
        var playerChangesLogHandleResponse = await handlerTest.Handle(logFilterRequest, _tokenTest);

        //Assert
        NotNull(playerChangesLogHandleResponse);
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
    public async Task LogRequestBaseHandler_GenerateResponseModels_GetPlayerChangesLogResponseDtoAsync()
    {
        // todo переделать!!
        //Arrange
        var handlerTest = new PlayerChangesLogRequestHandler(
            _handlersMock.MediatorMock(LogRequestBaseHandlerResponsesFake.GetSendHandleResponse(),
                LogRequestBaseHandlerResponsesFake.GetSendResponseModelsResponse()),
            _handlersMock.LocalizerMock(LogRequestBaseHandlerResponsesFake.GetTestTryLocalizeResponse()), new RequestContext());

        IEnumerable<PlayerChangesLogDomainModel> domainModels =
            LogRequestBaseHandlerResponsesFake.GetTestPlayerChangesLogDomainModel();
        var playerChangesLogResponseDto = LogRequestBaseHandlerTestRequests.GetPlayerChangesLogResponseDtoTestRequest();

        //Act
        var responseInvoke =
            (await handlerTest.Invoke<Task<IEnumerable<PlayerChangesLogResponseDto>>>("GenerateResponseModelsAsync",
                domainModels, FakeValues.LanguageTest, _tokenTest)).ToList();

        ////Assert
        Equal(playerChangesLogResponseDto.SerializeToString(), responseInvoke.SerializeToString());
    }

    /// <summary>
    /// Testing GenerateResponseModelsAsync Method - not all Key
    /// </summary>
    [Fact]
    public async Task LogRequestBaseHandler_GenerateResponseModels_GetNoLocalizedKeyAsync()
    {
        //Arrange


        // todo переделать!!
        var handlerTest = new PlayerChangesLogRequestHandler(
            _handlersMock.MediatorMock(null!, LogRequestBaseHandlerResponsesFake.GetSendResponseModelsResponse()),
            _handlersMock.LocalizerMock(LogRequestBaseHandlerResponsesFake.GetTestTryLocalizeResponseNotAllKey()), new RequestContext());
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
    public void LogRequestBaseHandler_GenerateResponseModelsAsyncThrowException_KeyNotFoundExceptionAsync()
    {
        //Arrange

        // todo переделать!!
        var handlerTest = new PlayerChangesLogRequestHandler(
            _handlersMock.MediatorMock(LogRequestBaseHandlerResponsesFake.GetSendHandleResponse(),
                LogRequestBaseHandlerResponsesFake.GetSendNoConcidenceKeyResponse()),
            _handlersMock.LocalizerMock(LogRequestBaseHandlerResponsesFake.GetTestTryLocalizeResponse()), new RequestContext());

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
    public async Task LogRequestBaseHandler_GenerateResponseGroupedModels_GetPlayerChangesLogResponseDtoAsync()
    {
        //Arrange

        // todo переделать!!
        var handlerTest = new PlayerChangesLogRequestHandler(
            _handlersMock.MediatorMock(LogRequestBaseHandlerResponsesFake.GetSendHandleResponse(),
                LogRequestBaseHandlerResponsesFake.GetSendResponseModelsResponse()),
            _handlersMock.LocalizerMock(LogRequestBaseHandlerResponsesFake.GetTestTryLocalizeResponse()), new RequestContext());

        IGrouping<ModuleName, PlayerChangesLogDomainModel> groupedModels = LogRequestBaseHandlerResponsesFake
            .GetTestPlayerChangesLogDomainModel().GroupBy(model => model.GetModuleName()).First();
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
    public async Task LogRequestBaseHandler_GenerateGroupedResponseModels_GetNoLocalizedKeyAsync()
    {
        //Arrange

        // todo переделать!!
        var handlerTest = new PlayerChangesLogRequestHandler(
            _handlersMock.MediatorMock(null!, LogRequestBaseHandlerResponsesFake.GetSendResponseModelsResponse()),
            _handlersMock.LocalizerMock(LogRequestBaseHandlerResponsesFake.GetTestTryLocalizeResponseNotAllKey()), new RequestContext());

        IGrouping<ModuleName, PlayerChangesLogDomainModel> groupedModels = LogRequestBaseHandlerResponsesFake
            .GetTestPlayerChangesLogDomainModel().GroupBy(model => model.GetModuleName()).First();
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

        // todo переделать!!
        var handlerTest = new PlayerChangesLogRequestHandler(
            _handlersMock.MediatorMock(LogRequestBaseHandlerResponsesFake.GetSendHandleResponse(),
                LogRequestBaseHandlerResponsesFake.GetSendNoConcidenceKeyResponse()),
            _handlersMock.LocalizerMock(LogRequestBaseHandlerResponsesFake.GetTestTryLocalizeResponse()), new RequestContext());

        IGrouping<ModuleName, PlayerChangesLogDomainModel> groupedModels = LogRequestBaseHandlerResponsesFake
            .GetTestPlayerChangesLogDomainModel().GroupBy(model => model.GetModuleName()).First();
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
    public async Task LogRequestBaseHandler_ELKSendLogFilterRequestDtoException_ExceptionHandled()
    {
        //Arrange

        // todo переделать!!
        var handlerTest = new PlayerChangesLogRequestHandler(_handlersMock.MediatorLogFilterRequestExceptionMock(),
            _handlersMock.LocalizerEmptyMock(), new RequestContext());

        //Assert
        await ThrowsAnyAsync<Exception>(() => handlerTest.Handle(LogRequestBaseHandlerTestRequests.GetTestLogFilterRequest(), _tokenTest));
    }

    /// <summary>
    /// Testing when ELK Send method with EventsRequest params returns Exception
    /// </summary>
    [Fact]
    public async Task LogRequestBaseHandler_ELKSendEventsRequestException_ExceptionHandled()
    {
        //Arrange

        // todo переделать!!
        var handlerTest = new PlayerChangesLogRequestHandler(_handlersMock.MediatorEventsRequestExceptionMock(),
            _handlersMock.LocalizerEmptyMock(), new RequestContext());

        //Assert
        await ThrowsAnyAsync<Exception>(() => handlerTest.Handle(LogRequestBaseHandlerTestRequests.GetTestLogFilterRequest(), _tokenTest));
    }

    /// <summary>
    /// Testing when ELK Send method with EventsRequest params returns Exception
    /// </summary>
    [Fact]
    public async Task LogRequestBaseHandler_LocalizerException_ExceptionHandled()
    {
        //Arrange

        // todo переделать!!
        var handlerTest = new PlayerChangesLogRequestHandler(_handlersMock.MediatorEmptyMock(),
            _handlersMock.LocalizerExceptionMock(), new RequestContext());

        //Assert
        await ThrowsAnyAsync<Exception>(() => handlerTest.Handle(LogRequestBaseHandlerTestRequests.GetTestLogFilterRequest(), _tokenTest));
    }

    #endregion
}