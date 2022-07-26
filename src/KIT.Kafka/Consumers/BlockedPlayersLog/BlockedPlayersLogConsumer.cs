using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using KIT.Kafka.Consumers.Base;
using KIT.Kafka.Settings.Interfaces;

namespace KIT.Kafka.Consumers.BlockedPlayersLog;

/// <summary>
///     Blocked players log consumer
/// </summary>
public class BlockedPlayersLogConsumer : BaseValidationConsumer<BlockedPlayersLogConsumerMessage>
{
    public BlockedPlayersLogConsumer(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Get the topic within which the consumer will listen to messages
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Topic for listening to messages</returns>
    protected override string GetTopic(IKafkaTopics kafkaTopics) => kafkaTopics.BlockedPlayersLog;

    /// <summary>
    ///     Get validated entity name
    /// </summary>
    /// <returns>Validated entity name</returns>
    protected override string GetValidatedEntityName() => "BlockedPlayersLog";

    /// <summary>
    ///     Get responsible service name
    /// </summary>
    /// <param name="context">The context of consumption</param>
    /// <returns>Responsible service name</returns>
    protected override string? GetResponsibleServiceName(ConsumeContext<BlockedPlayersLogConsumerMessage> context) => ModuleName.SSO.Description();
}