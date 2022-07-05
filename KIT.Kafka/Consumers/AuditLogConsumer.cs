using KIT.Kafka.Settings.Interfaces;
using Tolar.Kafka;

namespace KIT.Kafka.Consumers;

/// <summary>
///     Audit log consumer
/// </summary>
public class AuditLogConsumer : BaseConsumer
{
    public AuditLogConsumer(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Callback function on push messages
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="args">Event args of received message</param>
    /// <returns>Task execution result</returns>
    protected override Task OnMessageReceivedAsync(object? sender, MessageReceivedEventArgs args)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Get the topic within which the consumer will listen to messages
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Topic for listening to messages</returns>
    protected override string GetTopic(IKafkaTopics kafkaTopics)
    {
        return kafkaTopics.AuditLog;
    }
}