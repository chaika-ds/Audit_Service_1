using Tolar.Kafka;

namespace KIT.Kafka.Consumers.Base;

/// <summary>
///     The context of consumption
/// </summary>
/// <typeparam name="TModel">Message model type</typeparam>
public class ConsumeContext<TModel> where TModel : class
{
    public ConsumeContext(MessageReceivedEventArgs originalContext, TModel? message)
    {
        OriginalContext = originalContext;
        Message = message;
    }

    /// <summary>
    ///     The original receive context
    /// </summary>
    public MessageReceivedEventArgs OriginalContext { get; }

    /// <summary>
    ///     Message to the consumer. Is a typed message.
    /// </summary>
    public TModel? Message { get; }
}