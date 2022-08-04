using AuditService.Common.Models.Domain.AuditLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Tests.Fakes;
using AuditService.Tests.Resources;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.AuditService.GetAuditLog;

/// <summary>
/// Test of AuditLogDomainRequestHandler
/// </summary>
public class ElasticSearchGetAuditLogTest
{
    /// <summary>
    ///     Check if the result is coming from elastic search
    /// </summary>
    [Fact]
    public async Task Check_if_the_result_is_coming_from_elastic_search()
    {
        //Arrange
        IServiceProvider serviceProvider =
            FakeServiceProvider.GetServiceProviderForLogHandlers<AuditLogTransactionDomainModel>(
                TestResources.ElasticSearchResponse, TestResources.DefaultIndex);

        var auditLogDomainRequestHandler = serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<AuditLogFilterDto, LogSortDto, AuditLogTransactionDomainModel>();

        //Act 
        var result = await auditLogDomainRequestHandler.Send(filter, new TaskCanceledException().CancellationToken);

        //Assert
        NotEmpty(result.List);
    }
}