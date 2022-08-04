using KIT.Kafka.Settings.Interfaces;
using KIT.NLog.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tolar.Kafka;

namespace KIT.Kafka.Consumers.Base;

/// <summary>
///     Base consumer for migrating messages from one topic to another
/// </summary>
/// <typeparam name="TSourceModelType">Source model type</typeparam>
/// <typeparam name="TDestinationModelType">Destination model type</typeparam>
public abstract class BaseMigrationConsumer<TSourceModelType, TDestinationModelType> : BaseConsumer<TSourceModelType>
    where TSourceModelType : class, new() where TDestinationModelType : class, new()
{
    private readonly ILogger<BaseMigrationConsumer<TSourceModelType, TDestinationModelType>> _logger;
    private readonly IKafkaProducer _kafkaProducer;

    protected BaseMigrationConsumer(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _logger = serviceProvider.GetRequiredService<ILogger<BaseMigrationConsumer<TSourceModelType, TDestinationModelType>>>();
        _kafkaProducer = serviceProvider.GetRequiredService<IKafkaProducer>();
    }

    /// <summary>
    ///     Get the topic whose posts will be the source
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Topic for listening to messages</returns>
    protected abstract string GetSourceTopic(IKafkaTopics kafkaTopics);

    /// <summary>
    ///     Get destination topic. Messages will be posted there.
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Destination topic</returns>
    protected abstract string GetDestinationTopic(IKafkaTopics kafkaTopics);

    /// <summary>
    ///     The function acts as a filter to determine if data needs to be migrated
    /// </summary>
    /// <param name="model">Source model type</param>
    /// <returns>Need to migrate message</returns>
    protected abstract bool NeedToMigrateMessage(TSourceModelType model);

    /// <summary>
    ///     Transform source model to destination model
    /// </summary>
    /// <param name="sourceModel">Source model</param>
    /// <returns>Destination model</returns>
    protected abstract TDestinationModelType TransformSourceModel(TSourceModelType sourceModel);

    /// <summary>
    ///     Consumer method for receiving/listening to messages
    /// </summary>
    /// <param name="context">The context of consumption</param>
    /// <returns>Task execution result</returns>
    protected override async Task ConsumeAsync(ConsumeContext<TSourceModelType> context)
    {
        var sourceTopic = GetDestinationTopic(KafkaTopics);
        var destinationTopic = GetDestinationTopic(KafkaTopics);

        try
        {
            if (context.Message is null)
                return;

            if (!NeedToMigrateMessage(context.Message))
                return;

            var transformModel = TransformSourceModel(context.Message);
            await _kafkaProducer.SendAsync(transformModel, destinationTopic);

        }
        catch (Exception ex)
        {
            _logger.LogException(ex, $"Migration of data from topic {sourceTopic} to topic {destinationTopic} failed!", context);
        }
    }

    /// <summary>
    ///     Get the topic within which the consumer will listen to messages
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Topic for listening to messages</returns>
    protected override string GetTopic(IKafkaTopics kafkaTopics) => GetSourceTopic(kafkaTopics);
}