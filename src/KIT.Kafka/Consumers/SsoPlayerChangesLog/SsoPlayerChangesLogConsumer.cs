using AuditService.Common.Consts;
using AuditService.Common.Enums;
using AuditService.Common.Models.Domain.VisitLog;
using KIT.Kafka.Consumers.Base;
using KIT.Kafka.Settings.Interfaces;

namespace KIT.Kafka.Consumers.SsoPlayerChangesLog;

/// <summary>
///     SSO player changes log consumer
/// </summary>
public class SsoPlayerChangesLogConsumer : BaseMigrationConsumer<SsoPlayerChangesLogConsumerMessage, VisitLogDomainModel>
{
    public SsoPlayerChangesLogConsumer(IServiceProvider serviceProvider) : base(serviceProvider)
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
    protected override bool NeedToMigrateMessage(SsoPlayerChangesLogConsumerMessage model) =>
        model.EventType == VisitLogConst.EventTypeAuthorization;

    /// <summary>
    ///     Transform source model to destination model
    /// </summary>
    /// <param name="sourceModel">Source model</param>
    /// <returns>Destination model</returns>
    protected override VisitLogDomainModel TransformSourceModel(SsoPlayerChangesLogConsumerMessage sourceModel)
        => new()
        {
            Ip = sourceModel.LastVisitIp,
            Authorization = sourceModel.PlayerAuthorization,
            Timestamp = sourceModel.EventDateTime,
            Type = VisitLogType.Player,
            ProjectId = sourceModel.ProjectId,
            PlayerId = sourceModel.PlayerId,
            Login = DefineLogin(sourceModel),
            HallId = sourceModel.HallId
        };

    /// <summary>
    ///     Define login
    /// </summary>
    /// <param name="sourceModel">Source model</param>
    /// <returns>Login</returns>
    private static string DefineLogin(SsoPlayerChangesLogConsumerMessage sourceModel)
    {
        if (!string.IsNullOrEmpty(sourceModel.Login))
            return sourceModel.Login;

        return sourceModel.Email ?? sourceModel.Phone!;
    }
}