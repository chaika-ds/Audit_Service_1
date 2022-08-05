using KIT.Kafka.Consumers.BlockedPlayersLog;

namespace AuditService.Tests.Fakes.Kafka.Validators;

/// <summary>
/// Data for testing BlockedPlayersLogConsumerMessageValidator
/// </summary>
internal class BlockedPlayersLogConsumerMessageValidatorTestData
{
    /// <summary>
    /// Get BlockedPlayersLogConsumer string params with testing values 
    /// </summary>
    /// <param name="stringValue">Testing string value</param>
    /// <returns>BlockedPlayersLogConsumerMessage</returns>
    internal static BlockedPlayersLogConsumerMessage GetBlockedPlayersLogStringConsumerMessage(string stringValue) =>
        new ()
        {
            HallName = stringValue,
            ProjectName = stringValue,
            LastVisitIpAddress = stringValue,
            Platform = stringValue,
            PlayerLogin = stringValue,
            Browser = stringValue,
            BrowserVersion = stringValue,
            Language = stringValue
        };

    /// <summary>
    /// Get BlockedPlayersLogConsumer Guid params with testing values 
    /// </summary>
    /// <param name="guidValue">guidValue</param>
    /// <returns>BlockedPlayersLogConsumerMessage</returns>
    internal static BlockedPlayersLogConsumerMessage GetBlockedPlayersLogGuidConsumerMessage(Guid guidValue) =>
        new ()
        {
            ProjectId = guidValue,
            HallId = guidValue,
            PlayerId = guidValue
        };


    /// <summary>
    /// Get BlockedPlayersLogConsumer DateTime params with testing values 
    /// </summary>
    /// <param name="dateTimeValue"></param>
    /// <returns>BlockedPlayersLogConsumerMessage</returns>
    internal static BlockedPlayersLogConsumerMessage
        GetBlockedPlayersLogDateTimeConsumerMessage(DateTime dateTimeValue) =>
        new ()
        {
            BlockingDate = dateTimeValue
        };
}