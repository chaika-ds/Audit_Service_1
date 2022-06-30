using AuditService.Setup.ConfigurationSettings;
using Microsoft.Extensions.Hosting;
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
    private readonly HostOptions _options;
    private IEnumerable<IHostedService> _hostedServices;
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
        catch (Exception ex)
        {
            throw;
        }
    }

    private static bool False(Action action) { action(); return false; }

    public override async Task StopAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("StopAsync start");
        // Create a cancellation token source that fires after ShutdownTimeout seconds
        using (var cts = new CancellationTokenSource(_options.ShutdownTimeout))
        using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, cancellationToken))
        {
            // Create a token, which is cancelled if the timer expires
            var token = linkedCts.Token;

            // Run StopAsync on each registered hosted service
            foreach (var hostedService in _hostedServices.Reverse())
            {
                // stop calling StopAsync if timer expires
                token.ThrowIfCancellationRequested();
                try
                {
                    await hostedService.StopAsync(token).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        await base.StopAsync(cancellationToken: cancellationToken);

        await Task.Delay(millisecondsDelay: 2_000,
            cancellationToken: CancellationToken.None);
        _logger.LogInformation("StopAsync");
    }
}