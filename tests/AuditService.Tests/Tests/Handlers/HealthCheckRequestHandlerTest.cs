using AuditService.Common.Consts;
using AuditService.Common.Models.Dto;
using AuditService.Handlers.Handlers;
using AuditService.Tests.Fakes.Setup.ELK;
using KIT.Kafka.HealthCheck;
using KIT.Redis.HealthCheck;
using MediatR;
using Moq;

namespace AuditService.Tests.Tests.Handlers;

/// <summary>
///     Allows you to get a list of available services and categories
/// </summary>
public class HealthCheckRequestHandlerTest
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IKafkaHealthCheck> _kafkaHcMock;
    private readonly Mock<IRedisHealthCheck> _redisHcMock;
    private readonly HealthCheckRequestHandler _handle;

    public HealthCheckRequestHandlerTest()
    {
        var elasticClient = ElasticSearchClientProviderFake.GetFakeElasticSearchClient("");
        _mediatorMock = new Mock<IMediator>();
        _kafkaHcMock = new Mock<IKafkaHealthCheck>();
        _redisHcMock = new Mock<IRedisHealthCheck>();
        
        _handle = new HealthCheckRequestHandler(_mediatorMock.Object, elasticClient, _kafkaHcMock.Object, _redisHcMock.Object);
    }

    /// <summary>
    ///     Testing Handle Method
    /// </summary>
    [Fact]
    public async Task CheckHandle_Result_SuccessAsync()
    {
        //Arrange 
        _kafkaHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _redisHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _mediatorMock.Setup(x => x.Send(It.IsAny<GitLabRequest>(), CancellationToken.None)).Returns(Task.FromResult(new GitLabVersionResponseDto()));

        //Act
        var response = await _handle.Handle(new CheckHealthRequest(), CancellationToken.None);

        //Assert
        _kafkaHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _redisHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _mediatorMock.Verify(x => x.Send(It.IsAny<GitLabRequest>(), CancellationToken.None), Times.Once());

        IsType<HealthCheckResponseDto>(response);
        Contains(HealthCheckConst.Kafka, response.Components);
        Contains(HealthCheckConst.Elk, response.Components);
        Contains(HealthCheckConst.Redis, response.Components);
        NotNull(response.GitLabVersionResponse);
    }

    /// <summary>
    ///     Testing Handle Method
    /// </summary>
    [Fact]
    public async Task CheckHandle_Result_KafkaThrowsErrorAsync()
    {
        //Arrange 
        _kafkaHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(() => throw new Exception("Kafka error"));
        _redisHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _mediatorMock.Setup(x => x.Send(It.IsAny<GitLabRequest>(), CancellationToken.None)).Returns(Task.FromResult(new GitLabVersionResponseDto()));

        //Act
        var ex = await ThrowsAsync<Exception>(async () => await _handle.Handle(new CheckHealthRequest(), CancellationToken.None));

        //Assert
        _kafkaHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _redisHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Never());
        _mediatorMock.Verify(x => x.Send(It.IsAny<GitLabRequest>(), CancellationToken.None), Times.Never());

        NotNull(ex.Message);
        IsType<Exception>(ex);
    }

    /// <summary>
    ///     Testing Handle Method
    /// </summary>
    [Fact]
    public async Task CheckHandle_Result_RedisThrowsErrorAsync()
    {
        //Arrange 
        _kafkaHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _redisHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(() => throw new Exception("Redis error"));
        _mediatorMock.Setup(x => x.Send(It.IsAny<GitLabRequest>(), CancellationToken.None)).Returns(Task.FromResult(new GitLabVersionResponseDto()));

        //Act
        var ex = await ThrowsAsync<Exception>(async () => await _handle.Handle(new CheckHealthRequest(), CancellationToken.None));

        //Assert
        _kafkaHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _redisHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _mediatorMock.Verify(x => x.Send(It.IsAny<GitLabRequest>(), CancellationToken.None), Times.Never());

        NotNull(ex.Message);
        IsType<Exception>(ex);
    }

    /// <summary>
    ///     Testing Handle Method
    /// </summary>
    [Fact]
    public async Task CheckHandle_Result_MediatorThrowsErrorAsync()
    {
        //Arrange 
        _kafkaHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _redisHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _mediatorMock.Setup(x => x.Send(It.IsAny<GitLabRequest>(), CancellationToken.None)).Returns(() => throw new Exception("Redis error"));

        //Act
        var ex = await ThrowsAsync<Exception>(async () => await _handle.Handle(new CheckHealthRequest(), CancellationToken.None));

        //Assert
        _kafkaHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _redisHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _mediatorMock.Verify(x => x.Send(It.IsAny<GitLabRequest>(), CancellationToken.None), Times.Once());

        NotNull(ex.Message);
        IsType<Exception>(ex);
    }
}