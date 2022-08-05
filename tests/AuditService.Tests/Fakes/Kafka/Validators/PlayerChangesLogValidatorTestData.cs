using AuditService.Common.Models.Domain.PlayerChangesLog;
using KIT.Kafka.Consumers.PlayerChangesLog;

namespace AuditService.Tests.Fakes.Kafka.Validators;

/// <summary>
/// Data for testing PlayerChangesLogValidator
/// </summary>
internal class PlayerChangesLogValidatorTestData
{
    /// <summary>
    /// Get PlayerChangesLog string params with testing values 
    /// </summary>
    /// <param name="stringValue">Testing string value</param>
    /// <returns>PlayerChangesLogConsumerMessage</returns>
    internal static PlayerChangesLogConsumerMessage GetPlayerChangesLogStringConsumerMessage(string stringValue) =>
        new ()
        {
            EventCode = stringValue,
            IpAddress = stringValue,
        };


    /// <summary>
    /// Get PlayerChangesLog Guid params with testing values 
    /// </summary>
    /// <param name="guidValue">Testing Guid value</param>
    /// <returns>PlayerChangesLogConsumerMessage</returns>
    internal static PlayerChangesLogConsumerMessage GetPlayerChangesLogGuidConsumerMessage(Guid guidValue) =>
        new()
        {
            NodeId = guidValue,
            ProjectId = guidValue,
            PlayerId = guidValue
        };


    /// <summary>
    /// Get PlayerChangesLog DateTime params with testing values 
    /// </summary>
    /// <param name="dateTimeValue">Testing DateTime value</param>
    /// <returns>PlayerChangesLogConsumerMessage</returns>
    internal static PlayerChangesLogConsumerMessage
        GetPlayerChangesLogDateTimeConsumerMessage(DateTime dateTimeValue) =>
        new()
        {
            Timestamp = dateTimeValue
        };


    /// <summary>
    /// Get PlayerChangesLog Old-New params with testing values 
    /// </summary>
    /// <param name="value">Testing Old-New values</param>
    /// <returns>PlayerChangesLogConsumerMessage</returns>
    internal static PlayerChangesLogConsumerMessage GetPlayerChangesLogOldNewValueConsumerMessage(
        Dictionary<string, PlayerAttributeDomainModel> value) =>
        new()
        {
            OldValues = value,
            NewValues = value
        };


    /// <summary>
    /// Get PlayerChangesLog User params with testing values 
    /// </summary>
    /// <param name="userValue">Testing User values</param>
    /// <returns>PlayerChangesLogConsumerMessage</returns>
    internal static PlayerChangesLogConsumerMessage GetPlayerChangesLogUserConsumerMessage(
        UserInitiatorDomainModel userValue) =>
        new()
        {
            User = userValue
        };

    /// <summary>
    /// Get UserInitiatorDomainModel string params with testing values 
    /// </summary>
    /// <param name="stringValue">Testing string values</param>
    /// <returns>UserInitiatorDomainModel</returns>
    internal static UserInitiatorDomainModel GetUserInitiatorDomainModelLogStringConsumerMessage(string stringValue) =>
        new()
        {
            Email = stringValue,
            UserAgent = stringValue,
        };

    /// <summary>
    /// Get UserInitiatorDomainModel Guid params with testing values 
    /// </summary>
    /// <param name="guidValue">Testing Guid values</param>
    /// <returns>UserInitiatorDomainModel</returns>
    internal static UserInitiatorDomainModel GetUserInitiatorDomainModelGuidConsumerMessage(Guid guidValue) =>
        new()
        {
            Id = guidValue

        };

    /// <summary>
    /// Get UserInitiatorDomainModelNames with testing values 
    /// </summary>
    /// <returns>Array of strings</returns>
    internal static List<string> GetUserInitiatorDomainModelNames() =>
        new()
        {
            $"User.{nameof(UserInitiatorDomainModel.Id)}",
            $"User.{nameof(UserInitiatorDomainModel.Email)}",
            $"User.{nameof(UserInitiatorDomainModel.UserAgent)}"
        };

    /// <summary>
    /// Get PlayerAttributeDomainModel string params with testing values 
    /// </summary>
    /// <param name="stringValue"></param>
    /// <returns>PlayerAttributeDomainModel</returns>
    internal static PlayerAttributeDomainModel GetPlayerAttributeDomainModel(string stringValue) =>
        new()
        {
            Value = stringValue,
            Type = stringValue
        };

}