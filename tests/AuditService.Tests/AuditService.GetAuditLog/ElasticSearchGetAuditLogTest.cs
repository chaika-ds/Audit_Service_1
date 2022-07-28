using AuditService.Common.Models.Domain.AuditLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers;
using AuditService.Setup.AppSettings;
using AuditService.Tests.AuditService.GetAuditLog.Models;
using AuditService.Tests.Factories.Fakes;
using AuditService.Tests.Resources;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Redis;

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

        var serviceProvider = GetServiceProvder();

        var auditLogDomainRequestHandler = serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<AuditLogFilterDto, LogSortDto, AuditLogTransactionDomainModel>();

        //Act 
        var result = await auditLogDomainRequestHandler.Send(filter, new TaskCanceledException().CancellationToken);


        //Assert
        Assert.True(result.List.Any());
    }     

    /// <summary>
    ///     Getting fake service provider 
    /// </summary>
    private IServiceProvider GetServiceProvder()
    {
        var services = new ServiceCollection();

        DiConfigure.RegisterServices(services);
        services.AddSingleton<IRedisRepository, FakeRedisReposetoryForCachePipelineBehavior>();
        services.AddScoped<IElasticIndexSettings, FakeElasticSearchSettings>();
        services.AddLogging();
        services.AddScoped(serviceProvider =>
        {
            return FakeElasticSearchClientProvider.GetFakeElasticSearchClient<AuditLogTransactionDomainModel>(TestResources.ElasticSearchResponse);
        });

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}
