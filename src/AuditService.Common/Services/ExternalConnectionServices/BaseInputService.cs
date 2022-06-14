using AuditService.Kafka.Args;
using AuditService.Kafka.Kafka;
using AuditService.Kafka.Services.Health;
using Microsoft.Extensions.Logging;
using Tolar.Kafka;

namespace AuditService.Kafka.Services.ExternalConnectionServices;

/// <summary>
/// Base service for Kafka consumers
/// </summary>
/// <typeparam name="TInput"></typeparam>
public abstract class BaseInputService<TInput> : IInputService
    where TInput : class, new()
{
    protected readonly ILogger _logger;
    protected readonly IKafkaConsumer _consumer;
    protected readonly IHealthMarkService _healthService;

    protected BaseInputService(
        ILogger logger,
        IKafkaConsumerFactory consumerFactory,
        IInputSettings<TInput> inputSettings,
        IHealthMarkService healthService)
    {
        _logger = logger;
        _consumer = consumerFactory.CreateConsumer(inputSettings.Topic);
        _healthService = healthService;
    }

    public void Start()
    {
        _consumer.MessageReceived += OnMessageReceivedAsync;
        _consumer.KafkaError += OnKafkaError;
        _consumer.Start();
    }

    public void Stop()
    {
        _consumer.Stop();
        _consumer.MessageReceived -= OnMessageReceivedAsync;
        _consumer.KafkaError -= OnKafkaError;
    }

    protected abstract Task OnMessageReceivedAsync(object sender, MessageReceivedEventArgs args);

    private void OnKafkaError(object sender, EventArgs e)
    {
        _healthService.MarkError();
        _logger.LogError("Kafka connection error");
    }
}
