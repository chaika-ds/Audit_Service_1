using AuditService.Common.Consts;
using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Tests.AuditService.KIT.Kafka.Fakes;
using AuditService.Tests.Fakes;
using bgTeam.Extensions;
using KIT.Kafka;
using KIT.Kafka.Consumers.SsoPlayerChangesLog;
using KIT.Kafka.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Kafka;

namespace AuditService.Tests.AuditService.KIT.Kafka.Consumers;

/// <summary>
///     SSO player changes log consumer Test
/// </summary>
public class SsoPlayerChangesLogConsumerTest : SsoPlayerChangesLogConsumer
{
    private readonly IKafkaTopics _kafkaTopics;
    
    public SsoPlayerChangesLogConsumerTest() : base(GetServiceProvider())
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
        
        Assert.Equal(_kafkaTopics.SsoPlayersChangesLog, result);
    }
    
    /// <summary>
    ///     Check if the result is Visitlog
    /// </summary>
    [Fact]
    public void Get_Destination_Topic_Return_Visit_Log()
    {
        var result = GetDestinationTopic(_kafkaTopics);
        
        Assert.Equal(_kafkaTopics.Visitlog, result);
    }
    
    /// <summary>
    ///     Check if the result is true
    /// </summary>
    [Fact]
    public void Need_To_Migrate_Message_Return_true()
    {
        var model = new SsoPlayerChangesLogConsumerMessage {  EventType = VisitLogConst.EventTypeAuthorization  };
        
        var result = NeedToMigrateMessage(model);
        
        Assert.True(result);
    }
    
    /// <summary>
    ///     Check if the result is false
    /// </summary>
    [Fact]
    public void Need_To_Migrate_Message_Return_false()
    {
        var model = new SsoPlayerChangesLogConsumerMessage ();
        
        var result = NeedToMigrateMessage(model);
        
        Assert.False(result);
    }
    
    /// <summary>
    ///     Check if the result is VisitLogDomainModel type
    /// </summary>
    [Fact]
    public void Transform_Source_Model_Return_Visit_Log_Domain_Model()
    {
        var model = new SsoPlayerChangesLogConsumerMessage ()
        {
            LastVisitIp = "0.0.0.0",
            PlayerAuthorization = new AuthorizationDataDomainModel()
            {
                Browser = "chrome",
                DeviceType = "Mobile",
                OperatingSystem = "Windows",
                AuthorizationType = ""
            },
            EventType = "Create",
            ProjectId = Guid.NewGuid(),
            PlayerId = Guid.NewGuid(),
            HallId = Guid.NewGuid()
        };
        
        var result = TransformSourceModel(model);
        
        Assert.Equal(model.LastVisitIp, result.Ip);
        Assert.Equal(model.PlayerAuthorization, result.Authorization);
        Assert.Equal(model.EventDateTime, result.Timestamp);
        Assert.Equal(VisitLogType.Player, result.Type);
        Assert.Equal(model.ProjectId, result.ProjectId);
        Assert.Equal(model.PlayerId, result.PlayerId);
        Assert.Equal(DefineLoginFake(model), result.Login);
        Assert.Equal(model.HallId, result.HallId);
        
        
        Assert.IsType<VisitLogDomainModel>(result);
    }

    
    /// <summary>
    ///     Define login
    /// </summary>
    /// <param name="sourceModel">Source model</param>
    /// <returns>Login</returns>
    private static string DefineLoginFake(SsoPlayerChangesLogConsumerMessage sourceModel)
    {
        if (!string.IsNullOrEmpty(sourceModel.Login))
            return sourceModel.Login;

        return sourceModel.Email ?? sourceModel.Phone!;
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