using AuditService.Common.Consts;
using AuditService.Tests.Fakes.Kafka.Messages;
using AuditService.Tests.Fakes.Kafka.Models;
using KIT.Kafka.Consumers.Base;
using KIT.Kafka.Settings.Interfaces;

namespace AuditService.Tests.Fakes.Kafka.Consumers;

/// <summary>
/// Base migration consumer fake
/// </summary>
public class BaseMigrationConsumerFake: BaseMigrationConsumer<BaseConsumerMessageFake, BaseVisitLogDomainModelFake>
{
    public BaseMigrationConsumerFake(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Get the topic whose posts will be the source
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Topic for listening to messages</returns>
    protected override string GetSourceTopic(IKafkaTopics kafkaTopics) => kafkaTopics.SsoPlayersChangesLog;

    /// <summary>
    ///     Get destination topic. Messages will be posted there.
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Destination topic</returns>
    protected override string GetDestinationTopic(IKafkaTopics kafkaTopics) => kafkaTopics.Visitlog;

    /// <summary>
    ///     The function acts as a filter to determine if data needs to be migrated
    /// </summary>
    /// <param name="model">Source model type</param>
    /// <returns>Need to migrate message</returns>
    protected override bool NeedToMigrateMessage(BaseConsumerMessageFake model) =>
        model.EventType == VisitLogConst.EventTypeAuthorization;

    /// <summary>
    ///     Transform source model to destination model
    /// </summary>
    /// <param name="sourceModel">Source model</param>
    /// <returns>Destination model</returns>
    protected override BaseVisitLogDomainModelFake TransformSourceModel(BaseConsumerMessageFake sourceModel)
        => new()
        {
            Timestamp = sourceModel.Timestamp
        };
}