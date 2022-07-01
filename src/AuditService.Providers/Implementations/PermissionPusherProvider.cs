using AuditService.Setup.ConfigurationSettings;
using Microsoft.Extensions.Hosting;
using Tolar.Authenticate;
using Tolar.Kafka;

namespace AuditService.Providers.Implementations;

public class PermissionPusherProvider : PermissionPusher
{
    private readonly IKafkaProducer _producer;
    private readonly IPermissionPusherSettings _settings;
    private readonly IHostEnvironment _environment;

    public PermissionPusherProvider(IKafkaProducer producer, IPermissionPusherSettings settings, IHostEnvironment environment) : base(settings.ServiceId, settings.ServiceName)
    {
        _producer = producer;
        _settings = settings;
        _environment = environment;
    }

    protected override async Task PushAsync(object obj)
    {
        await _producer.SendAsync(obj, _settings.Topic);
    }
}