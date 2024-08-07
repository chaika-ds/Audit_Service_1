using AuditService.Common.Consts;
using AuditService.Tests.Fakes.Kafka.Consumers;
using AuditService.Tests.Fakes.Kafka.Messages;
using AuditService.Tests.Fakes.Kafka.Models;
using AuditService.Tests.Fakes.Kafka.Producers;
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
    private static readonly IServiceProvider ServiceProvider = GetServiceProvider();
    
    public BaseMigrationConsumerTest() : base(ServiceProvider)
    {
    }

    /// <summary>
    ///     Check if the result is SsoPlayersChangesLog
    /// </summary>
    [Fact]
    public void GetSourceTopic_Return_SsoPlayerChangesLog()
    {
        var result = GetSourceTopic(KafkaTopics);

        Equal(KafkaTopics.SsoUsersChangesLog, result);
    }

    /// <summary>
    ///     Check if the result is Visitlog
    /// </summary>
    [Fact]
    public void GetDestinationTopic_Return_VisitLog()
    {
        var result =  GetDestinationTopic(KafkaTopics);

        Equal(KafkaTopics.Visitlog, result);
    }

    /// <summary>
    ///     Check if the result is true
    /// </summary>
    [Fact]
    public void NeedToMigrateMessage_Return_true()
    {
        var model = new BaseConsumerMessageFake() {EventType = VisitLogConst.EventTypeAuthorization};

        var result = NeedToMigrateMessage(model);

        True(result);
    }

    /// <summary>
    ///     Check if the result is false
    /// </summary>
    [Fact]
    public void NeedToMigrateMessage_Return_false()
    {
        var model = new BaseConsumerMessageFake();

        var result =  NeedToMigrateMessage(model);

        False(result);
    }

    /// <summary>
    ///     Check if the result is VisitLogDomainModel type
    /// </summary>
    [Fact]
    public void TransformSourceModel_Return_VisitLogDomainModel()
    {
        var model = new BaseConsumerMessageFake()
        {
            Timestamp = DateTime.Now,
            EventType = "Create"
        };

        var result =  TransformSourceModel(model);
        
        Equal(model.Timestamp, result.Timestamp);
        IsType<BaseVisitLogDomainModelFake>(result);
    }
    
    /// <summary>
    ///     ConsumeAsync method will be tested and SendAsync will be executed at least one time
    /// </summary>
    [Fact]
    public void ConsumeAsync_AllOk_Result_SendAsyncExecuted()
    {
        var fakeModel = new BaseConsumerMessageFake()
        {
            Timestamp = DateTime.Now,
            EventType = VisitLogConst.EventTypeAuthorization,
        };
        
        NeedToMigrateMessage(fakeModel);
        
        var message = new MessageReceivedEventArgs(1, 1, null, "fakeModel", DateTime.Now);

        var result = ConsumeAsync(new ConsumeContext<BaseConsumerMessageFake>(message, fakeModel));
        
        True(KafkaProducersFake.IsSendAsyncExecuted);
        NotNull(result);
    }
    
    /// <summary>
    ///     ConsumeAsync method will be tested and SendAsync will be not executed
    /// </summary>
    [Fact]
    public void ConsumeAsync_If_NeedToMigrateMessage_Not_Called_Result_SendAsync_NotExecuted()
    {
        var fakeModel = new BaseConsumerMessageFake()
        {
            Timestamp = DateTime.Now,
        };

        var fakeMessage = new MessageReceivedEventArgs(1, 1, null, "fakeModel", DateTime.Now);

        var result = ConsumeAsync(new ConsumeContext<BaseConsumerMessageFake>(fakeMessage, fakeModel));

        False(KafkaProducersFake.IsSendAsyncExecuted);
        NotNull(result);
    }

    /// <summary>
    ///     ConsumeAsync method will be tested and SendAsync will be not executed
    /// </summary>
    [Fact]
    public void ConsumeAsync_IfMessageNull_Result_SendAsync_NotCalled()
    {
        var fakeMessage = new MessageReceivedEventArgs(1, 1, null, "fakeModel", DateTime.Now);

        var result = ConsumeAsync(new ConsumeContext<BaseConsumerMessageFake>(fakeMessage, null));

        False(KafkaProducersFake.IsSendAsyncExecuted);
        NotNull(result);
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
        
        services.AddScoped<IKafkaProducer, KafkaProducersFake>();

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}