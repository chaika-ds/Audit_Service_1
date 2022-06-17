using AuditService.Setup.Interfaces;
using Tolar.Authenticate;
using Tolar.Kafka;

namespace AuditService.Providers.Implementations;

public class PermissionPusherProvider : PermissionPusher
{
    private readonly IKafkaProducer _producer;
    private readonly IPermissionPusher _settings;

    public PermissionPusherProvider(IKafkaProducer producer, IPermissionPusher settings) : base(settings.ServiceIdentificator, settings.ServiceName)
    {
        _producer = producer;
        _settings = settings;
    }

    protected override Task PushAsync(object obj)
    {
        return _producer.SendAsync(obj, _settings.TopicOfKafka);
    }
}