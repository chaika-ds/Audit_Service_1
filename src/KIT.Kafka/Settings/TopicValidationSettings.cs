using KIT.Kafka.Settings.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KIT.Kafka.Settings;

/// <summary>
///     Topic validation settings
/// </summary>
internal class TopicValidationSettings : ITopicValidationSettings
{
    public TopicValidationSettings(IConfiguration config) => ValidationResultChat = config["Kafka:TopicValidation:ValidationResultChat"];

    /// <summary>
    ///     Chat for topic validation results
    /// </summary>
    public string? ValidationResultChat { get; set; }
}