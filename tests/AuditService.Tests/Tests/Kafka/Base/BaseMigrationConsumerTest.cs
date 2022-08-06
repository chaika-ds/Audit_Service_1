using AuditService.Common.Consts;
using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Tests.Fakes.Kafka.Consumers;
using AuditService.Tests.Fakes.Kafka.Messages;
using AuditService.Tests.Fakes.Kafka.Models;
using AuditService.Tests.Fakes.ServiceData;
using bgTeam.Extensions;
using KIT.Kafka;
using KIT.Kafka.Consumers.Base;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tolar.Kafka;

namespace AuditService.Tests.Tests.Kafka.Base;


/// <summary>
///     Base Migration Consumer Test
/// </summary>
public class BaseMigrationConsumerTest : BaseMigrationConsumerFake
{
    public BaseMigrationConsumerTest() : base(GetServiceProvider()) { }

    /// <summary>
    ///     Check if the result is SsoPlayersChangesLog
    /// </summary>
    [Fact]
    public void Get_Source_Topic_Return_Sso_Player_Changes_Log()
    {
        var result = GetSourceTopic(KafkaTopics);

        Assert.Equal(KafkaTopics.SsoUsersChangesLog, result);
    }

    /// <summary>
    ///     Check if the result is Visitlog
    /// </summary>
    [Fact]
    public void Get_Destination_Topic_Return_Visit_Log()
    {
        var result =  GetDestinationTopic(KafkaTopics);

        Assert.Equal(KafkaTopics.Visitlog, result);
    }

    /// <summary>
    ///     Check if the result is true
    /// </summary>
    [Fact]
    public void Need_To_Migrate_Message_Return_true()
    {
        var model = new BaseConsumerMessageFake() {EventType = VisitLogConst.EventTypeAuthorization};

        var result = NeedToMigrateMessage(model);

        Assert.True(result);
    }

    /// <summary>
    ///     Check if the result is false
    /// </summary>
    [Fact]
    public void Need_To_Migrate_Message_Return_false()
    {
        var model = new BaseConsumerMessageFake();

        var result =  NeedToMigrateMessage(model);

        Assert.False(result);
    }

    /// <summary>
    ///     Check if the result is VisitLogDomainModel type
    /// </summary>
    [Fact]
    public void Transform_Source_Model_Return_Visit_Log_Domain_Model()
    {
        var model = new BaseConsumerMessageFake()
        {
            Timestamp = DateTime.Now,
            EventType = "Create"
        };

        var result =  TransformSourceModel(model);
        
        Assert.Equal(model.Timestamp, result.Timestamp);
        Assert.IsType<VisitLogDomainModel>(result);
    }
    
    /// <summary>
    ///     ConsumeAsync method will be tested and SendAsync will be executed at least one time
    /// </summary>
    [Fact]
    public void Consume_Async_Result_Send_Async_Executed()
    {
        var mockProvider = new Mock<IKafkaProducer>();
        
        var fakeModel = new BaseConsumerMessageFake()
        {
            Timestamp = DateTime.Now,
            EventType = VisitLogConst.EventTypeAuthorization,
        };

        NeedToMigrateMessage(fakeModel);
        
        mockProvider.Setup(x =>  x.SendAsync(It.IsAny<BaseVisitLogDomainModelFake>(), It.IsAny<string>(), CancellationToken.None)).Returns(Task.CompletedTask);
        
        var message = new MessageReceivedEventArgs(1, 1, null, "fakeModel", DateTime.Now);

        var result = ConsumeAsync(new ConsumeContext<BaseConsumerMessageFake>(message, fakeModel));

        mockProvider.Verify(x => x.SendAsync(It.IsAny<BaseVisitLogDomainModelFake>(), It.IsAny<string>(), CancellationToken.None), Times.Exactly(1));
        
        Assert.NotNull(result);
    }

    /// <summary>
    ///     ConsumeAsync method will be tested and SendAsync will be not executed
    /// </summary>
    [Fact]
    public void Consume_Async_Result_Send_Async_Not_Executed_If_NeedToMigrateMessage_Not_Called()
    {
        var mockProvider = new Mock<IKafkaProducer>();
        
        var fakeModel = new BaseConsumerMessageFake()
        {
            Timestamp = DateTime.Now,
            EventType = VisitLogConst.EventTypeAuthorization,
        };

        mockProvider.Setup(x => x.SendAsync(It.IsAny<BaseVisitLogDomainModelFake>(), It.IsAny<string>(), CancellationToken.None)).Returns(Task.CompletedTask);
        
        var fakeMessage = new MessageReceivedEventArgs(1, 1, null, "fakeModel", DateTime.Now);

        var result = ConsumeAsync(new ConsumeContext<BaseConsumerMessageFake>(fakeMessage, fakeModel));

        mockProvider.Verify(x => x.SendAsync(It.IsAny<BaseVisitLogDomainModelFake>(), It.IsAny<string>(), CancellationToken.None), Times.Never);
        
        Assert.NotNull(result);
    }
    
    /// <summary>
    ///     ConsumeAsync method will be tested and SendAsync will be not executed
    /// </summary>
    [Fact]
    public void Consume_Async_Result_Send_Async_Not_Called_Null_If_Message_Null()
    {
        var mockProvider = new Mock<IKafkaProducer>();

        mockProvider.Setup(x => x.SendAsync(It.IsAny<BaseVisitLogDomainModelFake>(), It.IsAny<string>(), CancellationToken.None)).Returns(Task.CompletedTask);
        
        var fakeMessage = new MessageReceivedEventArgs(1, 1, null, "fakeModel", DateTime.Now);

        var result = ConsumeAsync(new ConsumeContext<BaseConsumerMessageFake>(fakeMessage, null));

        mockProvider.Verify(x => x.SendAsync(It.IsAny<BaseVisitLogDomainModelFake>(), It.IsAny<string>(), CancellationToken.None), Times.Never);
        
        Assert.NotNull(result);
    }

    /// <summary>
    ///     Getting fake service provider 
    /// </summary>
    private static IServiceProvider GetServiceProvider()
    {
        var services = ServiceCollectionFake.CreateServiceCollectionFake();

        services.ConfigureKafka("fakeEnv");

        services.AddLogging();

        services.AddSettings<IKafkaConsumerSettings, KafkaConsumerSettingsFake>();

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}