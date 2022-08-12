using AuditService.Common.Models.Domain;
using KIT.Kafka.Consumers.AuditLog;

namespace AuditService.Tests.Fakes.Kafka.Validators;

/// <summary>
/// Data for testing AuditLogConsumerMessageValidator
/// </summary>
internal class AuditLogValidatorTestData
{
    /// <summary>
    /// Get AuditLogConsumerMessage string params with testing values 
    /// </summary>
    /// <param name="stringValue">Testing string value</param>
    /// <returns>AuditLogConsumerMessage</returns>
    internal static AuditLogConsumerMessage GetAuditLogConsumerStringMessage(string stringValue) =>
        new()
        {
            CategoryCode = stringValue,
        };

    /// <summary>
    /// Get AuditLogConsumerMessage Guid params with testing values 
    /// </summary>
    /// <param name="guidValue">Testing Guid value</param>
    /// <returns>AuditLogConsumerMessage</returns>
    internal static AuditLogConsumerMessage GetAuditLogConsumerGuidMessage(Guid guidValue) =>
        new()
        {
            NodeId = guidValue
        };

    /// <summary>
    /// Get AuditLogConsumerMessage DateTime params with testing values 
    /// </summary>
    /// <param name="dateTimeValue">Testing DateTime value</param>
    /// <returns>AuditLogConsumerMessage</returns>
    internal static AuditLogConsumerMessage
        GetAuditLogDateTimeConsumerMessage(DateTime dateTimeValue) =>
        new()
        {
            Timestamp = dateTimeValue
        };

    /// <summary>
    /// Get AuditLogConsumerMessage User params with testing values 
    /// </summary>
    /// <param name="userRoleValue">Testing User values</param>
    /// <returns>VisitLogConsumerMessage</returns>
    internal static AuditLogConsumerMessage GetVisitLogUserConsumerMessage(
        IdentityUserDomainModel userRoleValue) =>
        new()
        {
            User = userRoleValue
        };

    /// <summary>
    /// Get IdentityUserDomainModel string params with testing values 
    /// </summary>
    /// <param name="stringValue">Testing string value</param>
    /// <returns>IdentityUserDomainModel</returns>
    internal static IdentityUserDomainModel GetIdentityUserDomainStringModel(string stringValue) =>
        new()
        {
            Ip = stringValue,
            Email = stringValue,
            UserAgent = stringValue
        };

    /// <summary>
    /// Get IdentityUserDomainModel Guid params with testing values 
    /// </summary>
    /// <param name="guidValue">Testing Guid value</param>
    /// <returns>IdentityUserDomainModel</returns>
    internal static IdentityUserDomainModel GetIdentityUserDomainGuidModel(Guid guidValue) =>
        new()
        {
            Id = guidValue
        };

    /// <summary>
    /// Get UserInitiatorDomainModelNames with testing values 
    /// </summary>
    /// <returns>Array of strings</returns>
    internal static List<string> GetIdentityUserDomainModelIpName() =>
        new()
        {
            $"User.{nameof(IdentityUserDomainModel.Ip)}"
        };
}