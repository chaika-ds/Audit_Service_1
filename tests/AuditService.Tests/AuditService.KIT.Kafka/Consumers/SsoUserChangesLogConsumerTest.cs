using AuditService.Common.Consts;
using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Tests.AuditService.KIT.Kafka.Fakes;
using AuditService.Tests.Fakes;
using bgTeam.Extensions;
using KIT.Kafka;
using KIT.Kafka.Consumers.SsoUserChangesLog;
using KIT.Kafka.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Kafka;

namespace AuditService.Tests.AuditService.KIT.Kafka.Consumers;

/// <summary>
///     SSO user changes log consumer Test
/// </summary>
public class SsoUserChangesLogConsumerTest : SsoUserChangesLogConsumer
{
    private readonly IKafkaTopics _kafkaTopics;


    public SsoUserChangesLogConsumerTest() : base(GetServiceProvider())
    {
        var serviceProvider = GetServiceProvider();
        _kafkaTopics = serviceProvider.GetRequiredService<IKafkaTopics>();
    }

    /// <summary>
    ///     Check if the result is SsoPlayersChangesLog
    /// </summary>
    [Fact]
    public void Get_Source_Topic_RETURN_Sso_Player_Changes_Log()
    {
        var result = GetSourceTopic(_kafkaTopics);

        Assert.Equal(_kafkaTopics.SsoUsersChangesLog, result);
    }

    /// <summary>
    ///     Check if the result is Visitlog
    /// </summary>
    [Fact]
    public void Get_Destination_Topic_RETURN_Visit_Log()
    {
        var result =  GetDestinationTopic(_kafkaTopics);

        Assert.Equal(_kafkaTopics.Visitlog, result);
    }

    /// <summary>
    ///     Check if the result is true
    /// </summary>
    [Fact]
    public void Need_To_Migrate_Message_RETURN_true()
    {
        var model = new SsoUserChangesLogConsumerMessage {EventType = VisitLogConst.EventTypeAuthorization};

        var result = NeedToMigrateMessage(model);

        Assert.True(result);
    }

    /// <summary>
    ///     Check if the result is false
    /// </summary>
    [Fact]
    public void Need_To_Migrate_Message_RETURN_false()
    {
        var model = new SsoUserChangesLogConsumerMessage();

        var result =  NeedToMigrateMessage(model);

        Assert.False(result);
    }

    /// <summary>
    ///     Check if the result is VisitLogDomainModel type
    /// </summary>
    [Fact]
    public void Transform_Source_Model_RETURN_Visit_Log_Domain_Model()
    {
        var model = new SsoUserChangesLogConsumerMessage();

        var result =  TransformSourceModel(model);

        Assert.IsType<VisitLogDomainModel>(result);
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