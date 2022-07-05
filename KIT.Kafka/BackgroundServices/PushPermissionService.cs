using KIT.Kafka.Settings.Interfaces;
using Tolar.Authenticate;
using Tolar.Kafka;

namespace KIT.Kafka.BackgroundServices;

/// <summary>
/// Background service for pushing permissions
/// </summary>
public class PushPermissionService : PermissionPusher
{
    private readonly IKafkaProducer _producer;
    private readonly IKafkaTopics _kafkaTopics;

    public PushPermissionService(IKafkaProducer producer, IPermissionPusherSettings settings,
        IKafkaTopics kafkaTopics) : base(settings.ServiceId, settings.ServiceName)
    {
        _producer = producer;
        _kafkaTopics = kafkaTopics;
    }

    /// <summary>
    /// Push permissions
    /// </summary>
    /// <param name="obj">Permissions</param>
    /// <returns>Push result</returns>
    protected override async Task PushAsync(object obj) => await _producer.SendAsync(obj, _kafkaTopics.Permissions);
}