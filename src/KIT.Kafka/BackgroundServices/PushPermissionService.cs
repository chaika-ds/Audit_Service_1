using KIT.Kafka.Settings.Interfaces;
using KIT.NLog.Extensions;
using Microsoft.Extensions.Logging;
using Tolar.Authenticate;
using Tolar.Kafka;

namespace KIT.Kafka.BackgroundServices;

/// <summary>
///     Background service for pushing permissions
/// </summary>
public class PushPermissionService : PermissionPusher
{
    private readonly IKafkaTopics _kafkaTopics;
    private readonly ILogger<PushPermissionService> _logger;
    private readonly IKafkaProducer _producer;

    public PushPermissionService(IKafkaProducer producer, IPermissionPusherSettings settings,
        IKafkaTopics kafkaTopics, ILogger<PushPermissionService> logger) : base(settings.ServiceId,
        settings.ServiceName)
    {
        _producer = producer;
        _kafkaTopics = kafkaTopics;
        _logger = logger;
    }

    /// <summary>
    ///     Push permissions
    /// </summary>
    /// <param name="obj">Permissions</param>
    /// <returns>Push result</returns>
    protected override async Task PushAsync(object obj)
    {
        try
        {
            await _producer.SendAsync(obj, _kafkaTopics.Permissions);
            _logger.LogInformation("Push permissions completed successfully", obj);
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Push permissions with an error", obj);
        }
    }
}