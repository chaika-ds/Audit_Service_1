using AuditService.Common.Extensions;
using KIT.Kafka.Consumers.Base;
using KIT.Kafka.Settings.Interfaces;

namespace KIT.Kafka.Consumers.PlayerChangesLog;

/// <summary>
///     Player changes log consumer
/// </summary>
public class PlayerChangesLogConsumer : BaseValidationConsumer<PlayerChangesLogConsumerMessage>
{
    public PlayerChangesLogConsumer(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Get the topic within which the consumer will listen to messages
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Topic for listening to messages</returns>
    protected override string GetTopic(IKafkaTopics kafkaTopics) => kafkaTopics.PlayerChangesLog;

    /// <summary>
    ///     Get validated entity name
    /// </summary>
    /// <returns>Validated entity name</returns>
    protected override string GetValidatedEntityName() => "PlayerChangesLog";

    /// <summary>
    ///     Get responsible service name
    /// </summary>
    /// <param name="context">The context of consumption</param>
    /// <returns>Responsible service name</returns>
    protected override string? GetResponsibleServiceName(ConsumeContext<PlayerChangesLogConsumerMessage> context)
    {
        var moduleName = context.Message?.ModuleName ?? GetModuleNameFromMessage(context.OriginalContext.Data);
        return moduleName?.Description();
    }
}