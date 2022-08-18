using AuditService.Common.Consts;
using AuditService.Common.Models.Dto;
using AuditService.Handlers.Handlers;
using AuditService.Tests.Fakes.ServiceData;
using AuditService.Tests.Fakes.Setup.ELK;
using KIT.Kafka.HealthCheck;
using KIT.Redis.HealthCheck;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AuditService.Tests.Tests.Handlers;

/// <summary>
///     Allows you to get a list of available services and categories
/// </summary>
public class HealthCheckRequestHandlerTest
{
    private readonly Mock<IKafkaHealthCheck> _kafkaHcMock;
    private readonly Mock<IRedisHealthCheck> _redisHcMock;
    private readonly Mock<IRequestHandler<GitLabRequest, GitLabVersionResponseDto>> _gitlabRequestMock;

    public HealthCheckRequestHandlerTest()
    {
        _kafkaHcMock = new Mock<IKafkaHealthCheck>();
        _redisHcMock = new Mock<IRedisHealthCheck>();
        _gitlabRequestMock = new Mock<IRequestHandler<GitLabRequest, GitLabVersionResponseDto>>();
    }

    /// <summary>
    ///     Testing Handle Method
    /// </summary>
    [Fact]
    public async Task CheckHandle_Result_SuccessAsync()
    {
        //Arrange 
        _gitlabRequestMock.Setup(handler => handler.Handle(new GitLabRequest(), CancellationToken.None)).Returns(Task.FromResult(new GitLabVersionResponseDto()));
        _kafkaHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _redisHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));

        var serviceProvider = ServiceProviderFake.GetServiceProviderForHealthCheckHandlers(_gitlabRequestMock.Object, _kafkaHcMock.Object, _redisHcMock.Object);
        
        var mediator = serviceProvider.GetRequiredService<IMediator>();

        //Act
        var response = await mediator.Send(new CheckHealthRequest(), CancellationToken.None);

        //Assert
        _kafkaHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _redisHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _gitlabRequestMock.Verify(x => x.Handle(It.IsAny<GitLabRequest>(), CancellationToken.None), Times.Once());

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
        _gitlabRequestMock.Setup(x => x.Handle(It.IsAny<GitLabRequest>(), CancellationToken.None)).Returns(Task.FromResult(new GitLabVersionResponseDto()));
    
        var serviceProvider = ServiceProviderFake.GetServiceProviderForHealthCheckHandlers(_gitlabRequestMock.Object, _kafkaHcMock.Object, _redisHcMock.Object);
        
        var mediator = serviceProvider.GetRequiredService<IMediator>();

        //Act
        var ex = await ThrowsAsync<Exception>(async () => await mediator.Send(new CheckHealthRequest(), CancellationToken.None));
    
        //Assert
        _kafkaHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _redisHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Never());
        _gitlabRequestMock.Verify(x => x.Handle(It.IsAny<GitLabRequest>(), CancellationToken.None), Times.Never());
    
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
        _gitlabRequestMock.Setup(x => x.Handle(It.IsAny<GitLabRequest>(), CancellationToken.None)).Returns(Task.FromResult(new GitLabVersionResponseDto()));
    
        var serviceProvider = ServiceProviderFake.GetServiceProviderForHealthCheckHandlers(_gitlabRequestMock.Object, _kafkaHcMock.Object, _redisHcMock.Object);
        
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        
        //Act
        var ex = await ThrowsAsync<Exception>(async () => await mediator.Send(new CheckHealthRequest(), CancellationToken.None));
    
        //Assert
        _kafkaHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _redisHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _gitlabRequestMock.Verify(x => x.Handle(It.IsAny<GitLabRequest>(), CancellationToken.None), Times.Never());
    
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
        _gitlabRequestMock.Setup(x => x.Handle(It.IsAny<GitLabRequest>(), CancellationToken.None)).Returns(() => throw new Exception("Gitlab  error"));
    
        var serviceProvider = ServiceProviderFake.GetServiceProviderForHealthCheckHandlers(_gitlabRequestMock.Object, _kafkaHcMock.Object, _redisHcMock.Object);
        
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        
        //Act
        var ex = await ThrowsAsync<Exception>(async () => await mediator.Send(new CheckHealthRequest(), CancellationToken.None));
    
        //Assert
        _kafkaHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _redisHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _gitlabRequestMock.Verify(x => x.Handle(It.IsAny<GitLabRequest>(), CancellationToken.None), Times.Once());
    
        NotNull(ex.Message);
        IsType<Exception>(ex);
    }
}