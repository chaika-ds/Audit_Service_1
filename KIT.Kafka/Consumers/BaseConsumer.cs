using KIT.Kafka.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Kafka;

namespace KIT.Kafka.Consumers;

/// <summary>
///     Base consumer of Kafka
/// </summary>
public abstract class BaseConsumer : IConsumer
{
    private readonly IKafkaConsumerFactory _consumerFactory;
    private readonly IKafkaTopics _kafkaTopics;
    private IKafkaConsumer? _consumer;

    protected BaseConsumer(IServiceProvider serviceProvider)
    {
        _consumerFactory = serviceProvider.GetRequiredService<IKafkaConsumerFactory>();
        _kafkaTopics = serviceProvider.GetRequiredService<IKafkaTopics>();
    }

    /// <summary>
    ///     Start consumer
    /// </summary>
    public void Start()
    {
        _consumer = _consumerFactory.CreateConsumer(GetTopic(_kafkaTopics));
        _consumer.MessageReceived += OnMessageReceivedAsync;
        _consumer.Start();
    }

    /// <summary>
    ///     Stop consumer
    /// </summary>
    public void Stop()
    {
        if (_consumer is null)
            return;

        _consumer.Stop();
        _consumer.MessageReceived -= OnMessageReceivedAsync;
    }

    /// <summary>
    ///     Consumer name
    /// </summary>
    public string Name => GetType().Name;

    /// <summary>
    ///     Callback function on push messages
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="args">Event args of received message</param>
    /// <returns>Task execution result</returns>
    protected abstract Task OnMessageReceivedAsync(object? sender, MessageReceivedEventArgs args);

    /// <summary>
    ///     Get the topic within which the consumer will listen to messages
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Topic for listening to messages</returns>
    protected abstract string GetTopic(IKafkaTopics kafkaTopics);
}