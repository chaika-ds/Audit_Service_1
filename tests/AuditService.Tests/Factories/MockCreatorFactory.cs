using AuditService.Tests.Factories.Enums;
using AuditService.Tests.Factories.Models;
using Moq;
using Nest;
using Tolar.Authenticate;
using Tolar.Kafka;
using Tolar.Redis;

namespace AuditService.Tests.Factories;

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
    
    public Mock CreateMockObject(MockCreator type, IEnumerable<BaseMock> input)
    {
        return type switch
        {
            MockCreator.REDIS => RedisMock(input.Cast<RedisMock>()),
            MockCreator.SSO => SsoMock(input.Cast<SsoMock>().FirstOrDefault()!),
            MockCreator.KAFKA => KafkaMock(),
            MockCreator.ELK => ElkMock(),
            _ => throw new Exception("type not exist")
        };
    }
    
    private Mock RedisMock(IEnumerable<RedisMock> input)
    {
        foreach (var item in input)
        {
            _mockRedisRepository.Setup(e => e.SetAsync(item.RedisKey, item.RedisValue, TimeSpan.FromMinutes(item.ExpireInMinute))).Returns(Task.FromResult(true));
            _mockRedisRepository.Setup(e => e.GetAsync<string>(item.RedisKey)).Returns(Task.FromResult(item.RedisValue)!);
        }
        
        return _mockRedisRepository;
    }
    
    private Mock SsoMock(SsoMock input)
    {
        _authenticateService.Setup(e => e.AuthenticationService()).Returns(Task.FromResult(input.ExpectedToken));
       
        _authenticateService.Setup(e => e.GetIsUserAuthenticate(input.Token, input.NodeId)).Returns(Task.FromResult(input.ExpectedObject));
        
        return _mockRedisRepository;
    }
    
    private Mock ElkMock()
    {
        return _elasticClient;
    }
    
    private Mock KafkaMock()
    {
        return _kafkaConsumer;
    }
}
