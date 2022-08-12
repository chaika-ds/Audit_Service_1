using AuditService.Common.Models.Domain.AuditLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Tests.Fakes.ServiceData;
using AuditService.Tests.Resources;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;

namespace AuditService.Tests.Tests.Journals.AuditLog;

/// <summary>
/// Test of AuditLogDomainRequestHandler
/// </summary>
public class ElasticSearchGetAuditLogTest
{
    /// <summary>
    ///     Check if the result is coming from elastic search
    /// </summary>
    [Fact]
    public async Task GetAuditLogs_CreateAuditLog_ResultWithAuditLogs()
    {
        //Arrange
        var serviceProvider = ServiceProviderFake.GetServiceProviderForLogHandlers<AuditLogDomainModel>(TestResources.ElasticSearchResponse, TestResources.DefaultIndex);

        var mediatorService = serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<AuditLogFilterDto, LogSortDto, AuditLogDomainModel>()
        {
            Filter = new ()
            {
                TimestampFrom = DateTime.Now,
                TimestampTo = DateTime.Now
            }
        };

        var result = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

        //Assert
        True(result.List.Any());
    }

    /// <summary>
    ///     Validation of audit log response
    /// </summary>
    [Fact]
    public async Task AuditLogResponseValidation_CreateAuditLog_HandlerResponseСorrespondsToTheExpected()
    {
        //Arrange
        var serviceProvider = ServiceProviderFake.GetServiceProviderForLogHandlers<AuditLogDomainModel>(TestResources.ElasticSearchResponse, TestResources.DefaultIndex);

        var mediatorService = serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<AuditLogFilterDto, LogSortDto, AuditLogDomainModel>()
        {
            Filter = new ()
            {
                TimestampFrom = DateTime.Now,
                TimestampTo = DateTime.Now
            }
        };

        var expected = JsonConvert.DeserializeObject<List<AuditLogDomainModel>>(Encoding.Default.GetString(TestResources.ElasticSearchResponse))
            ?.FirstOrDefault();

        //Act 
        var result = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

        var actual = result.List.FirstOrDefault(x => x.EntityId == expected.EntityId);

        //Assert
        Equal(expected.Timestamp, actual.Timestamp);
        Equal(expected.ActionName, actual.ActionName);
        Equal(expected.EntityName, actual.EntityName);
        Equal(expected.ModuleName, actual.ModuleName);
        Equal(expected.NodeId, actual.NodeId);
        Equal(expected.CategoryCode, actual.CategoryCode);
        Equal(expected.RequestUrl, actual.RequestUrl);
        Equal(expected.RequestBody, actual.RequestBody);
        Equal(expected.OldValue, actual.OldValue);
        Equal(expected.NewValue, actual.NewValue);
    }
}