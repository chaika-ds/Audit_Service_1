using System.Text;
using AuditService.Common.Helpers;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Setup.AppSettings;
using AuditService.Tests.Fakes.Handlers;
using AuditService.Tests.Fakes.Journals;
using AuditService.Tests.Fakes.ServiceData;
using AuditService.Tests.Resources;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AuditService.Tests.Tests.Journals.PlayerChangesLog;

/// <summary>
/// PlayerChangesLogDomainRequestHandler Test class
/// </summary>
public class PlayerChangesLogDomainRequestHandlerTest
{

    private readonly IElasticIndexSettings _elasticIndexSettings;
    private static IServiceProvider _serviceProvider = null!;
    private readonly PlayerChangesLogDomainRequestHandlerFake _handlerTest;
    private readonly byte[] _playerChangesLogDomainModel;


    public PlayerChangesLogDomainRequestHandlerTest()
    {
        _playerChangesLogDomainModel =
            Encoding.UTF8.GetBytes(LogRequestBaseHandlerResponsesFake.GetTestPlayerChangesLogDomainModel().SerializeToString());
        _serviceProvider = ServiceProviderFake.GetServiceProviderForLogHandlers<PlayerChangesLogDomainModel>(_playerChangesLogDomainModel, TestResources.PlayerChangesLog);
        _elasticIndexSettings = _serviceProvider.GetRequiredService<IElasticIndexSettings>();
        _handlerTest = new PlayerChangesLogDomainRequestHandlerFake(_serviceProvider);
    }

    /// <summary>
    /// Testing for accepting any result from Send method with PlayerChangesLog filters
    /// </summary>
    [Fact]
    public async Task Send_PlayerChangesLogFilterSend_ReturnsNotEmptyResultAsync()
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var mediatorService = _serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogDomainModel>();

        //Act 
        var result = await mediatorService.Send(filter, cts.Token);

        //Assert
        NotEmpty(result.List);
    }

    /// <summary>
    ///     Validation of player changes log response
    /// </summary>
    [Fact]
    public async Task PlayerChangesLogResponseValidation_CreatePlayerChangesLog_HandlerResponseСorrespondsToTheExpected()
    {
        //Arrange
        var mediatorService = _serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogDomainModel>();

        var expected = JsonConvert.DeserializeObject<List<PlayerChangesLogDomainModel>>(Encoding.Default.GetString(_playerChangesLogDomainModel))             
            ?.FirstOrDefault();

        //Act 
        var result = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

        var actual = result.List.FirstOrDefault(x => x.PlayerId == expected.PlayerId);

        //Assert
        Equal(expected.ModuleName, actual.ModuleName);
        Equal(expected.NodeId, actual.NodeId);
        Equal(expected.ProjectId, actual.ProjectId);
        Equal(expected.ModuleName, actual.ModuleName);
        Equal(expected.EventCode, actual.EventCode);
        Equal(expected.EventInitiator, actual.EventInitiator);
        Equal(expected.Timestamp, actual.Timestamp);
        Equal(expected.PlayerId, actual.PlayerId);
        Equal(expected.IpAddress, actual.IpAddress);
        Equal(expected.Reason, actual.Reason);
        if (expected.User != null)
        {
            Equal(expected.User.Id, actual.User.Id);
            Equal(expected.User.Email, actual.User.Email);
            Equal(expected.User.UserAgent, actual.User.UserAgent);
        };
    }

    /// <summary>
    /// Testing for getting Query index from Elastic search
    /// </summary>
    [Fact]
    public void GetQueryIndex_IndexFromElastic_ReturnQueryIndex()
    {
        //Act
        var queryIndex = _handlerTest.GetQueryIndexFake(_elasticIndexSettings);

        //Assert
        NotNull(queryIndex);
        Equal(TestResources.PlayerChangesLog, queryIndex!);
    }

    /// <summary>
    /// Testing for getting Query index from Elastic search
    /// </summary>
    [Fact]
    public void GetColumnNameToSort_NameOfPlayerChangesLogDomainModelTimestamp_ReturnNameOfColumnToSort()
    {
        //Arrange
        var logSortModelTest = new LogSortDto();

        //Act
        var columnName = _handlerTest.GetColumnNameToSortFake(logSortModelTest);

        //Assert
        NotNull(columnName);
        Equal(nameof(PlayerChangesLogDomainModel.Timestamp).ToLower(), columnName);
    }

}