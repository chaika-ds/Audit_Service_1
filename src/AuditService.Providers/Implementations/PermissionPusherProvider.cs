using AuditService.Setup.ConfigurationSettings;
using AuditService.Utility.Helpers;
using AuditService.Utility.Logger;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        AuditServiceConsoleLoggerExtension.WriteToLog(LogLevel.Information, _environment.EnvironmentName.ToLower(), $"Start push permissions. Topic: {_settings.Topic}", "PermissionPusherProvider");
        AuditServiceConsoleLoggerExtension.WriteToLog(LogLevel.Information, _environment.EnvironmentName.ToLower(), $"Permissions: {obj.SerializeToString()}", "PermissionPusherProvider");

        await _producer.SendAsync(obj, _settings.Topic);

        AuditServiceConsoleLoggerExtension.WriteToLog(LogLevel.Information, _environment.EnvironmentName.ToLower(), "All permissions are pushed", "PermissionPusherProvider");
    }
}