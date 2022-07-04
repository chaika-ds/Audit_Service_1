using AuditService.Kafka.AppSetings;
using Tolar.Authenticate;
using Tolar.Kafka;

namespace AuditService.Kafka.Services.ExternalConnectionServices;

/// <summary>
/// Background service for pushing permissions
/// </summary>
public class PushPermissionBackgroundService : PermissionPusher
{
    private readonly IKafkaProducer _producer;
    private readonly IPermissionPusherSettings _settings;

    public PushPermissionBackgroundService(IKafkaProducer producer, IPermissionPusherSettings settings) : base(settings.ServiceId, settings.ServiceName)
    {
        _producer = producer;
        _settings = settings;
    }

    /// <summary>
    /// Push permissions
    /// </summary>
    /// <param name="obj">Permissions</param>
    /// <returns>Push result</returns>
    protected override async Task PushAsync(object obj) => await _producer.SendAsync(obj, _settings.Topic);
}