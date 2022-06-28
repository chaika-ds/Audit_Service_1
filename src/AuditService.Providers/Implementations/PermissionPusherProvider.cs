using AuditService.Setup.ConfigurationSettings;
using Microsoft.Extensions.Logging;
using Tolar.Authenticate;
using Tolar.Authenticate.Dtos;
using Tolar.Kafka;

namespace AuditService.Providers.Implementations;

public class PermissionPusherProvider : PermissionPusher
{
    private readonly IKafkaProducer _producer;
    private readonly IPermissionPusherSettings _settings;
    private readonly ILogger<PermissionPusherProvider> _logger;

    public PermissionPusherProvider(
        IKafkaProducer producer,
        IPermissionPusherSettings settings,
        IAuthSsoServiceSettings ssoSettings,
        ILogger<PermissionPusherProvider> logger)
        : base(settings.ServiceId, ssoSettings.ServiceName)
    {
        _producer = producer;
        _settings = settings;
        _logger = logger;
    }

    protected override Task PushAsync(object obj)
    {
        try
        {
            return _producer.SendAsync(obj, _settings.Topic)
            .ContinueWith(c =>
                 _logger.LogInformation($"Permissions: {((Permission)obj).Action} | was sended on topik: {_settings.Topic}"));
        }
        catch (Exception ex) when (False(() => _logger.LogCritical(ex, "Fatal error")))
        {
            throw;
        }
    }

    private static bool False(Action action) { action(); return false; }

}