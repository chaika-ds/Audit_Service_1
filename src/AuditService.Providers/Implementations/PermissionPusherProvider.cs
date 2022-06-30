using AuditService.Setup.ConfigurationSettings;
using AuditService.Utility.Helpers;
using Microsoft.Extensions.Logging;
using Tolar.Authenticate;
using Tolar.Kafka;

namespace AuditService.Providers.Implementations;

public class PermissionPusherProvider : PermissionPusher
{
    private readonly IKafkaProducer _producer;
    private readonly IPermissionPusherSettings _settings;
    private readonly ILogger _logger;

    public PermissionPusherProvider(IKafkaProducer producer, IPermissionPusherSettings settings, ILogger logger) : base(settings.ServiceId, settings.ServiceName)
    {
        _producer = producer;
        _settings = settings;
        _logger = logger;
    }

    protected override async Task PushAsync(object obj)
    {
        _logger.LogInformation($"Start push permissions. Topic: {_settings.Topic}");
        _logger.LogInformation($"Permissions: {obj.SerializeToString()}");

        await _producer.SendAsync(obj, _settings.Topic);

        _logger.LogInformation("All permissions are pushed");
    }
}