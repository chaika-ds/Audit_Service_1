using AuditService.Kafka.Services.Health;
using Microsoft.Extensions.Logging;
using Tolar.Kafka;

namespace AuditService.Kafka.Services.ExternalConnectionServices;

/// <summary>
///     Base service for Kafka consumers
/// </summary>
public abstract class BaseInputService : IInputService
{
    private readonly IKafkaConsumer _consumer;
    private readonly IHealthMarkService _healthService;
    private readonly ILogger _logger;

    protected BaseInputService(
        ILogger logger,
        IKafkaConsumerFactory consumerFactory,
        IKafkaTopics kafkaTopics,
        IHealthMarkService healthService)
    {
        _logger = logger;
        _consumer = consumerFactory.CreateConsumer(kafkaTopics.AuditLog);
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

    protected abstract Task OnMessageReceivedAsync(object? sender, MessageReceivedEventArgs args);

    private void OnKafkaError(object? sender, EventArgs e)
    {
        _healthService.MarkError();
        // todo наверное надо расширить логи
        _logger.LogError("Kafka connection error");
    }
}