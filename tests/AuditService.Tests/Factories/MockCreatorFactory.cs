using AuditService.Tests.Factories.Models;
using Moq;
using Nest;
using Tolar.Authenticate;
using Tolar.Authenticate.Dtos;
using Tolar.Kafka;
using Tolar.Redis;

namespace AuditService.Tests.Factories;

/// <summary>
///     Mock Creator Factory
/// </summary>
public class MockCreatorFactory
{
    private readonly Mock<IRedisRepository> _mockRedisRepository;
    private readonly Mock<IAuthenticateService> _authenticateService;
    private readonly Mock<IKafkaConsumer> _kafkaConsumer;
    private readonly Mock<IElasticClient> _elasticClient;

    public MockCreatorFactory()
    {
        _authenticateService = new Mock<IAuthenticateService>();
        _mockRedisRepository = new Mock<IRedisRepository>();
        _kafkaConsumer = new Mock<IKafkaConsumer>();
        _elasticClient = new Mock<IElasticClient>();
    }
    
    /// <summary>
    ///     Create mock object
    /// </summary>
    /// <param name="type">Define type of Mock</param>
    /// <param name="input">Input params of Mock</param>
    public Mock CreateMockObject<TModel>(IEnumerable<BaseMock> input)
    {
        return typeof(TModel).Name switch
        {
            nameof(IRedisRepository) => RedisMock(input.Cast<RedisMock>()),
            nameof(IAuthenticateService) => SsoMock(input.Cast<SsoMock>().FirstOrDefault()!),
            nameof(IKafkaConsumer) => KafkaMock(),
            nameof(IElasticClient) => ElkMock(),
            _ => throw new Exception("type not exist")
        };
    }
    
    /// <summary>
    ///     Create Redis Mock
    /// </summary>
    /// <param name="input">Input params of Mock</param>
    private Mock RedisMock(IEnumerable<RedisMock> input)
    {
        foreach (var item in input)
        {
            _mockRedisRepository.Setup(e => e.SetAsync(item.RedisKey, item.RedisValue, TimeSpan.FromMinutes(item.ExpireInMinute))).Returns(Task.FromResult(true));
            _mockRedisRepository.Setup(e => e.GetAsync<string>(item.RedisKey)).Returns(Task.FromResult(item.RedisValue)!);
        }
        
        return _mockRedisRepository;
    }
    
    /// <summary>
    ///     Create Sso Mock
    /// </summary>
    /// <param name="input">Input params of Mock</param>
    private Mock SsoMock(SsoMock input)
    {
        _authenticateService.Setup(e => e.AuthenticationService()).Returns(Task.FromResult(input.ExpectedObject));
       
        _authenticateService.Setup(e => e.GetIsUserAuthenticate(input.Token, input.NodeId)).Returns(Task.FromResult(input.ExpectedObject as AuthenticatedResponse));
        
        return _mockRedisRepository;
    }
    
    /// <summary>
    ///     Create Elk Mock
    /// </summary>
    private Mock ElkMock()
    {
        return _elasticClient;
    }
    
    /// <summary>
    ///     Create Kafka Mock
    /// </summary>
    private Mock KafkaMock()
    {
        return _kafkaConsumer;
    }
}
