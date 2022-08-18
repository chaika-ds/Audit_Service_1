using AuditService.Common.Consts;
using AuditService.Common.Models.Dto;
using AuditService.Handlers.Handlers;
using AuditService.Tests.Fakes.ServiceData;
using AuditService.Tests.Fakes.Setup.ELK;
using KIT.Kafka.HealthCheck;
using KIT.Minio.HealthCheck;
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
    private readonly Mock<IMinioHealthCheck> _minioHealthCheckMock;
    private readonly Mock<IRequestHandler<GitLabRequest, GitLabVersionResponseDto>> _gitlabRequestMock;

    public HealthCheckRequestHandlerTest()
    {
        _kafkaHcMock = new Mock<IKafkaHealthCheck>();
        _redisHcMock = new Mock<IRedisHealthCheck>();
        _minioHealthCheckMock = new Mock<IMinioHealthCheck>();
        _gitlabRequestMock = new Mock<IRequestHandler<GitLabRequest, GitLabVersionResponseDto>>();
    }

    /// <summary>
    ///     Testing All Success case 
    /// </summary>
    [Fact]
    public async Task CheckHandle_Result_SuccessAsync()
    {
        //Arrange 
        _gitlabRequestMock.Setup(handler => handler.Handle(new GitLabRequest(), CancellationToken.None)).Returns(Task.FromResult(new GitLabVersionResponseDto()));
        _kafkaHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _redisHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _minioHealthCheckMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));

        var serviceProvider = ServiceProviderFake.GetServiceProviderForHealthCheckHandlers(_gitlabRequestMock.Object, _kafkaHcMock.Object, _redisHcMock.Object, _minioHealthCheckMock.Object);
        
        var mediator = serviceProvider.GetRequiredService<IMediator>();

        //Act
        var response = await mediator.Send(new CheckHealthRequest(), CancellationToken.None);

        //Assert
        _kafkaHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _redisHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _minioHealthCheckMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        _gitlabRequestMock.Verify(x => x.Handle(It.IsAny<GitLabRequest>(), CancellationToken.None), Times.Once());

        IsType<HealthCheckResponseDto>(response);
        Contains(HealthCheckConst.Kafka, response.Components);
        Contains(HealthCheckConst.Elk, response.Components);
        Contains(HealthCheckConst.Redis, response.Components);
        NotNull(response.Version);
    }

    /// <summary>
    ///     Testing Kafka Error case
    /// </summary>
    [Fact]
    public async Task CheckHandle_Result_KafkaThrowsErrorAsync()
    {
        //Arrange 
        _kafkaHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(() => throw new Exception("Kafka error"));
        _redisHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _minioHealthCheckMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _gitlabRequestMock.Setup(x => x.Handle(It.IsAny<GitLabRequest>(), CancellationToken.None)).Returns(Task.FromResult(new GitLabVersionResponseDto()));
        
        var serviceProvider = ServiceProviderFake.GetServiceProviderForHealthCheckHandlers(_gitlabRequestMock.Object, _kafkaHcMock.Object, _redisHcMock.Object, _minioHealthCheckMock.Object);
        
        var mediator = serviceProvider.GetRequiredService<IMediator>();

        //Act
        var ex = await ThrowsAsync<Exception>(async () => await mediator.Send(new CheckHealthRequest(), CancellationToken.None));
    
        //Assert
        _kafkaHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        NotNull(ex.Message);
        IsType<Exception>(ex);
    }
    
    /// <summary>
    ///     Testing Redis Error case
    /// </summary>
    [Fact]
    public async Task CheckHandle_Result_RedisThrowsErrorAsync()
    {
        //Arrange 
        _kafkaHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _redisHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(() => throw new Exception("Redis error"));
        _minioHealthCheckMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _gitlabRequestMock.Setup(x => x.Handle(It.IsAny<GitLabRequest>(), CancellationToken.None)).Returns(Task.FromResult(new GitLabVersionResponseDto()));
    
        var serviceProvider = ServiceProviderFake.GetServiceProviderForHealthCheckHandlers(_gitlabRequestMock.Object, _kafkaHcMock.Object, _redisHcMock.Object, _minioHealthCheckMock.Object);
        
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        
        //Act
        var ex = await ThrowsAsync<Exception>(async () => await mediator.Send(new CheckHealthRequest(), CancellationToken.None));
    
        //Assert
        _redisHcMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        NotNull(ex.Message);
        IsType<Exception>(ex);
    }
    
    /// <summary>
    ///     Testing Mediator Error case
    /// </summary>
    [Fact]
    public async Task CheckHandle_Result_MediatorThrowsErrorAsync()
    {
        //Arrange 
        _kafkaHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _redisHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _minioHealthCheckMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _gitlabRequestMock.Setup(x => x.Handle(It.IsAny<GitLabRequest>(), CancellationToken.None)).Returns(() => throw new Exception("Gitlab  error"));
    
        var serviceProvider = ServiceProviderFake.GetServiceProviderForHealthCheckHandlers(_gitlabRequestMock.Object, _kafkaHcMock.Object, _redisHcMock.Object, _minioHealthCheckMock.Object);
        
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        
        //Act
        var ex = await ThrowsAsync<Exception>(async () => await mediator.Send(new CheckHealthRequest(), CancellationToken.None));
    
        //Assert
        _gitlabRequestMock.Verify(x => x.Handle(It.IsAny<GitLabRequest>(), CancellationToken.None), Times.Once());
        NotNull(ex.Message);
        IsType<Exception>(ex);
    }
    
    /// <summary>
    ///     Testing Minio Error case
    /// </summary>
    [Fact]
    public async Task CheckHandle_Result_MinioThrowsErrorAsync()
    {
        //Arrange 
        _kafkaHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _redisHcMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(Task.FromResult(new HealthCheckComponentsDto()));
        _minioHealthCheckMock.Setup(x => x.CheckHealthAsync(CancellationToken.None)).Returns(() => throw new Exception("Minio  error"));
        _gitlabRequestMock.Setup(x => x.Handle(It.IsAny<GitLabRequest>(), CancellationToken.None)).Returns(Task.FromResult(new GitLabVersionResponseDto()));

        
        var serviceProvider = ServiceProviderFake.GetServiceProviderForHealthCheckHandlers(_gitlabRequestMock.Object, _kafkaHcMock.Object, _redisHcMock.Object, _minioHealthCheckMock.Object);
        
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        
        //Act
        var ex = await ThrowsAsync<Exception>(async () => await mediator.Send(new CheckHealthRequest(), CancellationToken.None));
    
        //Assert
        _minioHealthCheckMock.Verify(x => x.CheckHealthAsync(CancellationToken.None), Times.Once());
        NotNull(ex.Message);
        IsType<Exception>(ex);
    }
}