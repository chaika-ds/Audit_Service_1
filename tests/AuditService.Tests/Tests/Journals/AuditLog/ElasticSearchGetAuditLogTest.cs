using AuditService.Common.Models.Domain.AuditLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Tests.Fakes.ServiceData;
using AuditService.Tests.Helpers.Journals;
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
        await LogsTestHelper<AuditLogFilterDto, LogSortDto, AuditLogDomainModel, AuditLogDomainModel>
               .CheckReturnResult(TestResources.DefaultIndex, TestResources.ElasticSearchAuditLogResponse);
    }

    /// <summary>
    ///     Validation of audit log response
    /// </summary>
    [Fact]
    public async Task AuditLogResponseValidation_CreateAuditLog_HandlerResponseСorrespondsToTheExpected()
    {
        //Arrange
        var expected = LogsTestHelper<AuditLogFilterDto, LogSortDto, AuditLogDomainModel, AuditLogDomainModel>
            .GetExpectedDomainModels(TestResources.BlockedPlayersLog, TestResources.BlockedPlayersLogResponse)
            ?.FirstOrDefault();

        //Act 
        var result = await LogsTestHelper<AuditLogFilterDto, LogSortDto, AuditLogDomainModel, AuditLogDomainModel>
            .GetLogHandlerResponse(TestResources.BlockedPlayersLog, TestResources.BlockedPlayersLogResponse);

        var actual = result.List.FirstOrDefault(x => x.EntityId == expected.EntityId);

        //Assert
        Equal(expected.Timestamp, actual.Timestamp);
        Equal(expected.ActionName, actual.ActionName);
        Equal(expected.EntityName, actual.EntityName);
        Equal(expected.ModuleName, actual.ModuleName);
        Equal(expected.NodeId, actual.NodeId);
        Equal(expected.CategoryCode, actual.CategoryCode);
        Equal(expected.RequestUrl, actual.RequestUrl);
    }
}