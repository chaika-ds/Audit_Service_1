using AuditService.Common.Consts;
using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Tests.Fakes.Kafka.Consumers;
using AuditService.Tests.Fakes.ServiceData;
using bgTeam.Extensions;
using KIT.Kafka;
using KIT.Kafka.Consumers.SsoUserChangesLog;
using KIT.Kafka.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Kafka;

namespace AuditService.Tests.Tests.Kafka.Consumers;

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
    public void Get_Source_Topic_Return_Sso_Player_Changes_Log()
    {
        var result = GetSourceTopic(_kafkaTopics);

        Assert.Equal(_kafkaTopics.SsoUsersChangesLog, result);
    }

    /// <summary>
    ///     Check if the result is Visitlog
    /// </summary>
    [Fact]
    public void Get_Destination_Topic_Return_Visit_Log()
    {
        var result =  GetDestinationTopic(_kafkaTopics);

        Assert.Equal(_kafkaTopics.Visitlog, result);
    }

    /// <summary>
    ///     Check if the result is true
    /// </summary>
    [Fact]
    public void Need_To_Migrate_Message_Return_true()
    {
        var model = new SsoUserChangesLogConsumerMessage {EventType = VisitLogConst.EventTypeAuthorization};

        var result = NeedToMigrateMessage(model);

        Assert.True(result);
    }

    /// <summary>
    ///     Check if the result is false
    /// </summary>
    [Fact]
    public void Need_To_Migrate_Message_Return_false()
    {
        var model = new SsoUserChangesLogConsumerMessage();

        var result =  NeedToMigrateMessage(model);

        Assert.False(result);
    }

    /// <summary>
    ///     Check if the result is VisitLogDomainModel type
    /// </summary>
    [Fact]
    public void Transform_Source_Model_Return_Visit_Log_Domain_Model()
    {
        var model = new SsoUserChangesLogConsumerMessage()
        {
            NodeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UserLogin = "test@gmail.com",
            UserRoles = new List<UserRoleDomainModel>(){ new ("","")},
            UserIp = "0.0.0.0",
            UserAuthorization = new AuthorizationDataDomainModel()
            {
                Browser = "chrome",
                DeviceType = "Mobile",
                OperatingSystem = "Windows",
                AuthorizationType = ""
            },
            Timestamp = DateTime.Now,
            EventType = "Create"
            
        };

        var result =  TransformSourceModel(model);

        Assert.Equal(model.NodeId, result.NodeId);
        Assert.Equal(model.UserId, result.UserId);
        Assert.Equal(model.UserLogin, result.Login);
        Assert.Equal(model.UserRoles, result.UserRoles);
        Assert.Equal(model.UserIp, result.Ip);
        Assert.Equal(model.UserAuthorization, result.Authorization);
        Assert.Equal(model.Timestamp, result.Timestamp);
        Assert.Equal(VisitLogType.User.ToString(), result.Type);

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