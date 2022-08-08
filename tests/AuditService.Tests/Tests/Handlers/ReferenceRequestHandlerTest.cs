using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Common.Models.Dto;
using AuditService.Handlers;
using AuditService.Tests.Fakes.ServiceData;
using AuditService.Tests.Fakes.Setup;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Tolar.Redis;

namespace AuditService.Tests.Tests.Handlers;

/// <summary>
/// ReferenceRequestHandler test
/// </summary>
public class ReferenceRequestHandlerTest
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Allows you to get a list of available services and categories
    /// </summary>
    public ReferenceRequestHandlerTest()
    {
        _mediator = GetServiceProvider().GetRequiredService<IMediator>();
    }

    /// <summary>
    ///     Testing Handle Method for auditLog/services endpoint
    /// </summary>
    [Fact]
    public async Task GetServicesAsync_Return_AllServicesAsync()
    {
        var enumList = Enum.GetValues<ModuleName>().Select(value => new EnumResponseDto(value.ToString(), value.Description()));
       
        var result = await _mediator.Send(new GetServicesRequest(), CancellationToken.None);

        Equal(JsonConvert.SerializeObject(enumList), JsonConvert.SerializeObject(result));
    }

    /// <summary>
    ///     Getting fake service provider 
    /// </summary>
    private static IServiceProvider GetServiceProvider()
    {
        var services = ServiceCollectionFake.CreateServiceCollectionFake();
        services.AddLogging();
        DiConfigure.RegisterServices(services);
        services.AddSingleton<IRedisRepository, RedisReposetoryForCachePipelineBehaviorFake>();
        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}