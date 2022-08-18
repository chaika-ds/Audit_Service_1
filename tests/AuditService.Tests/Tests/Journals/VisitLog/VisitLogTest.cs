using AuditService.Common.Enums;
using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Filter.VisitLog;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Common.Models.Dto.VisitLog;
using AuditService.Tests.Fakes.ServiceData;
using AuditService.Tests.Resources;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;

namespace AuditService.Tests.Tests.Journals.VisitLog;

/// <summary>
///     Test of PlayerVisitLogDomainRequestHandler
/// </summary> 
public class VisitLogTest
{
    /// <summary>
    ///     Check if the result is coming from players visit log domain handler
    /// </summary>
    [Fact]
    public async Task GetPlayerVisiLogs_CreateVisitLog_ResultWithPlayerDomainLogs()
    {
        //Arrange
        var serviceProvider = ServiceProviderFake
            .GetServiceProviderForLogHandlers<PlayerVisitLogDomainModel>(TestResources.ElasticSearchVisitLogResponse, TestResources.VisitLog);

        var mediatorService = serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel>()
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
        True(result.List.Any());
    }

    /// <summary>
    ///     Validation of player visit log response
    /// </summary>
    [Fact]
    public async Task PlayerVisiLogResponseValidation_CreateVisitLog_HandlerResponseСorrespondsToTheExpected()
    {
        //Arrange
        var serviceProvider = ServiceProviderFake
            .GetServiceProviderForLogHandlers<PlayerVisitLogDomainModel>(TestResources.ElasticSearchVisitLogResponse, TestResources.VisitLog);

        var mediatorService = serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel>()
        {
            Filter = new ()
            {
                TimestampFrom = DateTime.Now,
                TimestampTo = DateTime.Now
            }
        };
        
        var expected = JsonConvert.DeserializeObject<List<PlayerVisitLogDomainModel>>(Encoding.Default.GetString(TestResources.ElasticSearchVisitLogResponse))
           ?.FirstOrDefault(x => x.Type == VisitLogType.Player.ToString());

        //Act 
        var result = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

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
    public async Task GetUserVisiLogs_CreateVisitLog_ResultWithUserDomainLogs()
    {
        //Arrange
        var serviceProvider = ServiceProviderFake
            .GetServiceProviderForLogHandlers<UserVisitLogDomainModel>(TestResources.ElasticSearchVisitLogResponse, TestResources.VisitLog);

        var mediatorService = serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogDomainModel>()
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
        True(result.List.Any());
    }

    /// <summary>
    ///     Validation of user visit log response
    /// </summary>
    [Fact]
    public async Task UserVisiLogResponseValidation_CreateVisitLog_HandlerResponseСorrespondsToTheExpected()
    {
        //Arrange
        var serviceProvider = ServiceProviderFake
            .GetServiceProviderForLogHandlers<UserVisitLogDomainModel>(TestResources.ElasticSearchVisitLogResponse, TestResources.VisitLog);

        var mediatorService = serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogDomainModel>()
        {
            Filter = new ()
            {
                TimestampFrom = DateTime.Now,
                TimestampTo = DateTime.Now
            }
        };
        
        var expected = JsonConvert.DeserializeObject<List<UserVisitLogDomainModel>>(Encoding.Default.GetString(TestResources.ElasticSearchVisitLogResponse))
           ?.FirstOrDefault(x => x.Type == VisitLogType.User.ToString());

        //Act 
        var result = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

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
    ///     Check if the result is coming from players visit log handler
    /// </summary>
    [Fact]
    public async Task GetPlayerVisiLogs_CreateVisitLog_ResultWithPlayerDroLogs()
    {
        //Arrange
        var serviceProvider = ServiceProviderFake
            .GetServiceProviderForLogHandlers<PlayerVisitLogDomainModel>(TestResources.ElasticSearchVisitLogResponse, TestResources.VisitLog);

        var mediatorService = serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogResponseDto>()
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
        True(result.List.Any());
    }

    /// <summary>
    ///     Check if the result is coming from users visit log handler
    /// </summary>
    [Fact]
    public async Task GetUserVisiLogs_CreateVisitLog_ResultWithUserDtoLogs()
    {
        //Arrange
        var serviceProvider = ServiceProviderFake
            .GetServiceProviderForLogHandlers<UserVisitLogDomainModel>(TestResources.ElasticSearchVisitLogResponse, TestResources.VisitLog);

        var mediatorService = serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogResponseDto>()
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
        True(result.List.Any());
    }

    /// <summary>
    ///     Check mapping user visit log from domain model to dto
    /// </summary>
    [Fact]
    public async Task CheckMappingUserVisitLogFromDomainModelToDto_CreateVisitLog_ResultWithUserDtoLogs()
    {
        //Arrange
        var serviceProvider = ServiceProviderFake
            .GetServiceProviderForLogHandlers<UserVisitLogDomainModel>(TestResources.ElasticSearchVisitLogResponse, TestResources.VisitLog);

        var mediatorService = serviceProvider.GetRequiredService<IMediator>();

        var filterForDomainModelHandler = new LogFilterRequestDto<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogDomainModel>()
        {
            Filter = new ()
            {
                TimestampFrom = DateTime.Now,
                TimestampTo = DateTime.Now
            }
        };
        
        var resultForDomainModelHandler = await mediatorService.Send(filterForDomainModelHandler, new TaskCanceledException().CancellationToken);

        var expected = resultForDomainModelHandler.List
            ?.FirstOrDefault(x => x.Type == Common.Enums.VisitLogType.User.ToString());

        var filter = new LogFilterRequestDto<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogResponseDto>()
        {
            Filter = new ()
            {
                TimestampFrom = DateTime.Now,
                TimestampTo = DateTime.Now
            }
        };
        //Act 
        var result = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

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
        var serviceProvider = ServiceProviderFake
            .GetServiceProviderForLogHandlers<PlayerVisitLogDomainModel>(TestResources.ElasticSearchVisitLogResponse, TestResources.VisitLog);

        var mediatorService = serviceProvider.GetRequiredService<IMediator>();

        var filterForDomainModelHandler = new LogFilterRequestDto<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel>()
        {
            Filter = new ()
            {
                TimestampFrom = DateTime.Now,
                TimestampTo = DateTime.Now
            }
        };
        
        var resultForDomainModelHandler = await mediatorService.Send(filterForDomainModelHandler, new TaskCanceledException().CancellationToken);

        var expected = resultForDomainModelHandler.List
            ?.FirstOrDefault(x => x.Type == Common.Enums.VisitLogType.Player.ToString());

        var filter = new LogFilterRequestDto<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogResponseDto>()
        {
            Filter = new ()
            {
                TimestampFrom = DateTime.Now,
                TimestampTo = DateTime.Now
            }
        };
        //Act 
        var result = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

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
