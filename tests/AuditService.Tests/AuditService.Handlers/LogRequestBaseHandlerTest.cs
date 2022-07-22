using AuditService.Common.Enums;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Pagination;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Handlers;
using static Xunit.Assert;

namespace AuditService.Tests.AuditService.Handlers;

/// <summary>
/// LogRequestBaseHandler test
/// </summary>
public class LogRequestBaseHandlerTest
    : HandlersMock<PlayerChangesLogFilterDto, LogSortDto,
        PlayerChangesLogDomainModel>
{
    private readonly ResponsesFake _responses;

    public LogRequestBaseHandlerTest()
    {
        _responses = new ResponsesFake();
    }

    /// <summary>
    /// Testing Handle Method
    /// </summary>
    [Fact]
    public async Task LogRequestBaseHandler_HandlerWorks_AllDataIsHandled()
    {
        //Arrange
        var handleSendResponse = _responses.GetSendHandleResponse();
        var responseModelsSendResponse = _responses.GetSendResponseModelsResponse();
        var tryLocalizeResponse = _responses.GetTestTryLocalizeResponse();

        var handlerTest = new PlayerChangesLogRequestHandler(MediatorMock(handleSendResponse, responseModelsSendResponse),
            LocalizerMock(tryLocalizeResponse));

        var logFilterRequest = GetTestLogFilterRequest();
        var cts = new CancellationTokenSource();

        //Act
        var playerChangesLogHandleResponse = await handlerTest.Handle(logFilterRequest, cts.Token);

        //Assert
        IsPlayerChangesLogReceived(playerChangesLogHandleResponse);
        IsEqualPaginationResponse(playerChangesLogHandleResponse.Pagination, handleSendResponse.Pagination);
        IsEqualPlayerChangesLogResponse(playerChangesLogHandleResponse.List.ToList(), handleSendResponse.List.ToList(),
            responseModelsSendResponse.Values.SelectMany(v => v.Select(x => x.Name)).ToArray());
        IsLocalizePlayerAttributeLocalized(playerChangesLogHandleResponse.List.ToList(),
            handleSendResponse.List.ToList(), tryLocalizeResponse);
    }

    /// <summary>
    /// Test data for LogFilterRequestDto request param input to Handler
    /// </summary>
    /// <returns>Test data for LogFilterRequestDto</returns>
    private LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogResponseDto>
        GetTestLogFilterRequest()
    {
        return
            new LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogResponseDto>
            {
                Filter = new PlayerChangesLogFilterDto
                {
                    Login = "TestLogin",
                    IpAddress = "000.000.000.000",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    EventKeys = new List<string>(),
                    Language = "ENG"
                    
                },
                Pagination = new PaginationRequestDto(),
                Sort = new LogSortDto()
                {
                    SortableType = SortableType.Ascending
                }
            };
    }
}