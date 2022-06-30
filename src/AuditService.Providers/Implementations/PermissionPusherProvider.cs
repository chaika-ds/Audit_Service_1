using AuditService.Setup.ConfigurationSettings;
using Tolar.Authenticate;
using Tolar.Kafka;

namespace AuditService.Providers.Implementations;

public class PermissionPusherProvider : PermissionPusher
{
    private readonly IKafkaProducer _producer;
    private readonly IPermissionPusherSettings _settings;

    public PermissionPusherProvider(IKafkaProducer producer, IPermissionPusherSettings settings) : base(settings.ServiceId, settings.ServiceName)
    {
        _producer = producer;
        _settings = settings;
    }

    protected override Task PushAsync(object obj)
    {
        return _producer.SendAsync(obj, _settings.Topic);
    }
}