using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using KIT.Kafka.Consumers.Base;
using KIT.Kafka.Settings.Interfaces;

namespace KIT.Kafka.Consumers.VisitLog;

/// <summary>
///     Visit log consumer
/// </summary>
public class VisitLogConsumer : BaseValidationConsumer<VisitLogConsumerMessage>
{
    public VisitLogConsumer(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Get the topic within which the consumer will listen to messages
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Topic for listening to messages</returns>
    protected override string GetTopic(IKafkaTopics kafkaTopics) => kafkaTopics.Visitlog;

    /// <summary>
    ///     Get validated entity name
    /// </summary>
    /// <returns>Validated entity name</returns>
    protected override string GetValidatedEntityName() => "VisitLog";

    /// <summary>
    ///     Get responsible service name
    /// </summary>
    /// <param name="context">The context of consumption</param>
    /// <returns>Responsible service name</returns>
    protected override string? GetResponsibleServiceName(ConsumeContext<VisitLogConsumerMessage> context)
    {
        if (context.Message is null)
            return ModuleName.SSO.Description();

        var sourceInfo = context.Message.Type == VisitLogType.Player ? "players-changes" : "users-changes";

        return $"{ModuleName.SSO.Description()}({sourceInfo})";
    }
}