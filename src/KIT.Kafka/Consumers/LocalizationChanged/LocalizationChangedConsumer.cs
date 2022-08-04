using AuditService.Localization.Localizer.Triggers;
using KIT.Kafka.Consumers.Base;
using KIT.Kafka.Settings.Interfaces;
using KIT.NLog.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KIT.Kafka.Consumers.LocalizationChanged;

/// <summary>
///     Localization changed consumer
/// </summary>
public class LocalizationChangedConsumer : BaseConsumer<LocalizationChangedConsumerMessage>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<LocalizationChangedConsumer> _logger;

    public LocalizationChangedConsumer(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _logger = serviceProvider.GetRequiredService<ILogger<LocalizationChangedConsumer>>();
    }

    /// <summary>
    ///     Consumer method for receiving/listening to messages
    /// </summary>
    /// <param name="context">The context of consumption</param>
    /// <returns>Task execution result</returns>
    protected override async Task ConsumeAsync(ConsumeContext<LocalizationChangedConsumerMessage> context)
    {
        if (context.Message is null)
        {
            _logger.LogError("Invalid message from the localization service", context.OriginalContext);
            return;
        }

        try
        {
            await _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IOnLocalizationChangesTrigger>()
                .PushChangesAsync(context.Message);
        }
        catch (Exception ex)
        {
           _logger.LogException(ex, "Localization changed consumer failed", context.OriginalContext);
        }
    }

    /// <summary>
    ///     Get the topic within which the consumer will listen to messages
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Topic for listening to messages</returns>
    protected override string GetTopic(IKafkaTopics kafkaTopics) => kafkaTopics.LocalizationChanged;
}