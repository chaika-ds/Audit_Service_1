using AuditService.Common.Consts;
using AuditService.Common.Models.Dto;
using AuditService.Handlers.Handlers;
using AuditService.Tests.Fakes.Setup.ELK;
using KIT.Kafka.HealthCheck;
using KIT.Redis.HealthCheck;
using MediatR;
using Moq;
using Nest;

namespace AuditService.Tests.Tests.Handlers;

/// <summary>
///     Allows you to get a list of available services and categories
/// </summary>
public class HealthCheckRequestHandlerTest
{
    private readonly IElasticClient _elasticClient;

    public HealthCheckRequestHandlerTest()
    {
        _elasticClient = ElasticSearchClientProviderFake.GetFakeElasticSearchClient("");
    }

    /// <summary>
    ///     Testing Handle Method
    /// </summary>
    [Fact]
    public async Task CheckHandle_Result_EachServiceCalledOnceAsync()
    {
        var mediatorMock = new Mock<IMediator>();
        var kafkaHcMock = new Mock<IKafkaHealthCheck>();
        var redisHcMock = new Mock<IRedisHealthCheck>();


        kafkaHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        redisHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        mediatorMock.Setup(x => x.Send(It.IsAny<GitLabRequest>(), CancellationToken.None)).Returns(Task.FromResult(new GitLabVersionResponseDto()));
        
        var handle = new HealthCheckRequestHandler(mediatorMock.Object, _elasticClient, kafkaHcMock.Object, redisHcMock.Object);
        var response = await handle.Handle(new CheckHealthRequest(), CancellationToken.None);

        
        kafkaHcMock.Verify(x =>x.CheckHealthAsync(CancellationToken.None), Times.Once());
        redisHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        mediatorMock.Verify(x => x.Send(It.IsAny<GitLabRequest>(), CancellationToken.None), Times.Once());
        
        IsType<HealthCheckResponseDto>(response);
        Contains(HealthCheckConst.Kafka, response.Components);
        Contains(HealthCheckConst.Elk, response.Components);
        Contains(HealthCheckConst.Redis, response.Components);
        NotNull(response.GitLabVersionResponse);
    }
}