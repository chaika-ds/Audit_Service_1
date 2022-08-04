using KIT.Kafka.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Tolar.Kafka;

namespace KIT.Kafka.Consumers.Base;

/// <summary>
///     Base consumer of Kafka
/// </summary>
/// <typeparam name="TModel">Message model type</typeparam>
public abstract class BaseConsumer<TModel> : IConsumer where TModel : class, new()
{
    private readonly IKafkaConsumerFactory _consumerFactory;
    private IKafkaConsumer? _consumer;
    protected readonly IKafkaTopics KafkaTopics;

    protected BaseConsumer(IServiceProvider serviceProvider)
    {
        _consumerFactory = serviceProvider.GetRequiredService<IKafkaConsumerFactory>();
        KafkaTopics = serviceProvider.GetRequiredService<IKafkaTopics>();
    }

    /// <summary>
    ///     Start consumer
    /// </summary>
    public void Start()
    {
        _consumer = _consumerFactory.CreateConsumer(GetTopic(KafkaTopics));
        _consumer.MessageReceived += OnMessageReceivedAsync;
        _consumer.Start();
    }

    /// <summary>
    ///     Stop consumer
    /// </summary>
    public void Stop()
    {
        if (_consumer is null)
            return;

        _consumer.Stop();
        _consumer.MessageReceived -= OnMessageReceivedAsync;
    }

    /// <summary>
    ///     Consumer name
    /// </summary>
    public string Name => GetType().Name;

    /// <summary>
    ///     Consumer method for receiving/listening to messages
    /// </summary>
    /// <param name="context">The context of consumption</param>
    /// <returns>Task execution result</returns>
    protected abstract Task ConsumeAsync(ConsumeContext<TModel> context);

    /// <summary>
    ///     Get the topic within which the consumer will listen to messages
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Topic for listening to messages</returns>
    protected abstract string GetTopic(IKafkaTopics kafkaTopics);

    /// <summary>
    ///     Callback function on push messages
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="args">Event args of received message</param>
    /// <returns>Task execution result</returns>
    private async Task OnMessageReceivedAsync(object? sender, MessageReceivedEventArgs args)
    {
        var message = GetMessageModel(args);
        await ConsumeAsync(new ConsumeContext<TModel>(args, message));
    }

    /// <summary>
    ///     Get typed message(model)
    /// </summary>
    /// <param name="args">Event args of received message</param>
    /// <returns>Typed message</returns>
    private static TModel? GetMessageModel(MessageReceivedEventArgs args)
    {
        try
        {
            return string.IsNullOrEmpty(args.Data) ? null : JsonConvert.DeserializeObject<TModel>(args.Data);
        }
        catch (Exception)
        {
            return null;
        }
    }
}