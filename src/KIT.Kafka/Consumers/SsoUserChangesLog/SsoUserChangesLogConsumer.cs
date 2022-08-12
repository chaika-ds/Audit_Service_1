using AuditService.Common.Consts;
using AuditService.Common.Enums;
using AuditService.Common.Models.Domain.VisitLog;
using KIT.Kafka.Consumers.Base;
using IKafkaTopics = KIT.Kafka.Settings.Interfaces.IKafkaTopics;

namespace KIT.Kafka.Consumers.SsoUserChangesLog;

/// <summary>
///     SSO user changes log consumer
/// </summary>
public class SsoUserChangesLogConsumer : BaseMigrationConsumer<SsoUserChangesLogConsumerMessage, VisitLogDomainModel>
{
    public SsoUserChangesLogConsumer(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Get the topic whose posts will be the source
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Topic for listening to messages</returns>
    protected override string GetSourceTopic(IKafkaTopics kafkaTopics) => kafkaTopics.SsoUsersChangesLog;

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
    protected override bool NeedToMigrateMessage(SsoUserChangesLogConsumerMessage model) =>
        model.EventType == VisitLogConst.EventTypeAuthorization;

    /// <summary>
    ///     Transform source model to destination model
    /// </summary>
    /// <param name="sourceModel">Source model</param>
    /// <returns>Destination model</returns>
    protected override VisitLogDomainModel TransformSourceModel(SsoUserChangesLogConsumerMessage sourceModel)
        => new()
        {
            NodeId = sourceModel.NodeId,
            UserId = sourceModel.UserId,
            Login = sourceModel.UserLogin,
            UserRoles = sourceModel.UserRoles,
            Ip = sourceModel.UserIp,
            Authorization = sourceModel.UserAuthorization,
            Timestamp = sourceModel.Timestamp,
            Type = VisitLogType.User
        };
}