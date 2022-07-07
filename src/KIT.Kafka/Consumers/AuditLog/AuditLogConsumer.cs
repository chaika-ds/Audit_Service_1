using KIT.Kafka.Consumers.Base;
using KIT.Kafka.Settings.Interfaces;

namespace KIT.Kafka.Consumers.AuditLog;

/// <summary>
/// Audit log consumer
/// </summary>
public class AuditLogConsumer : BaseConsumer<AuditLogConsumerMessage>
{
    public AuditLogConsumer(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Consumer method for receiving/listening to messages
    /// </summary>
    /// <param name="context">The context of consumption</param>
    /// <returns>Task execution result</returns>
    protected override Task Consume(ConsumeContext<AuditLogConsumerMessage> context)
    {
        // todo Реализация
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Get the topic within which the consumer will listen to messages
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Topic for listening to messages</returns>
    protected override string GetTopic(IKafkaTopics kafkaTopics) => kafkaTopics.AuditLog;
}