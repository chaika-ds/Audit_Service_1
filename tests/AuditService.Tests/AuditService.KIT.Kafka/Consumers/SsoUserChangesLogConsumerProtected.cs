using AuditService.Common.Models.Domain.VisitLog;
using KIT.Kafka.Consumers.SsoPlayerChangesLog;
using KIT.Kafka.Consumers.SsoUserChangesLog;
using KIT.Kafka.Settings.Interfaces;

namespace AuditService.Tests.AuditService.KIT.Kafka.Consumers;

public class SsoUserChangesLogConsumerProtected :SsoUserChangesLogConsumer
{
    public SsoUserChangesLogConsumerProtected(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }
    
    /// <summary>
    ///     Get the topic whose posts will be the source
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Topic for listening to messages</returns>
    public new string GetSourceTopic(IKafkaTopics kafkaTopics)
    {
        return base.GetSourceTopic(kafkaTopics);
    }

    /// <summary>
    ///     Get destination topic. Messages will be posted there.
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Destination topic</returns>
    public new string GetDestinationTopic(IKafkaTopics kafkaTopics)
    {
        return base.GetDestinationTopic(kafkaTopics);
    }

    /// <summary>
    ///     The function acts as a filter to determine if data needs to be migrated
    /// </summary>
    /// <param name="model">Source model type</param>
    /// <returns>Need to migrate message</returns>
    public new bool NeedToMigrateMessage(SsoUserChangesLogConsumerMessage model)
    {
        return base.NeedToMigrateMessage(model);
    }

    /// <summary>
    ///     Transform source model to destination model
    /// </summary>
    /// <param name="sourceModel">Source model</param>
    /// <returns>Destination model</returns>
    public new VisitLogDomainModel TransformSourceModel(SsoUserChangesLogConsumerMessage sourceModel)
    {
        return base.TransformSourceModel(sourceModel);
    }
}