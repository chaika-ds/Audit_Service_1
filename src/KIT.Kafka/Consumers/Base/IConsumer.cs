namespace KIT.Kafka.Consumers.Base;

/// <summary>
///     Message consumer
/// </summary>
public interface IConsumer
{
    /// <summary>
    ///     Consumer name
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     Start consumer
    /// </summary>
    void Start();

    /// <summary>
    ///     Stop consumer
    /// </summary>
    void Stop();
}