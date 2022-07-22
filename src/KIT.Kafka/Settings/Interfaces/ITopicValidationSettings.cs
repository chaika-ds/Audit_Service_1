namespace KIT.Kafka.Settings.Interfaces;

/// <summary>
///     Topic validation settings
/// </summary>
public interface ITopicValidationSettings
{
    /// <summary>
    ///     Chat for topic validation results
    /// </summary>
    string? ValidationResultChat { get; set; }
}