using AuditService.Common.Models.Domain.AuditLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Tests.Fakes;
using AuditService.Tests.Resources;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.AuditService.AuditLog;

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
        var serviceProvider = FakeServiceProvider.GetServiceProviderForLogHandlers<AuditLogTransactionDomainModel>(TestResources.ElasticSearchResponse, TestResources.DefaultIndex);

        var auditLogDomainRequestHandler = serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<AuditLogFilterDto, LogSortDto, AuditLogTransactionDomainModel>();

        //Act 
        var result = await auditLogDomainRequestHandler.Send(filter, new TaskCanceledException().CancellationToken);

        //Assert
        Assert.True(result.List.Any());
    }     
}
