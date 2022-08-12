using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.VisitLog;
using KIT.Kafka.Consumers.VisitLog;

namespace AuditService.Tests.Fakes.Kafka.Validators;

/// <summary>
/// Data for testing AuthorizationDataDomainModelValidator
/// </summary>
internal class VisitLogValidatorTestData
{
    /// <summary>
    /// Get AuthorizationDataDomainModel string params with testing values 
    /// </summary>
    /// <param name="stringValue">Testing string value</param>
    /// <returns>AuthorizationDataDomainModel</returns>
    internal static AuthorizationDataDomainModel GetAuthorizationDataDomainStringModel(string stringValue) =>
        new()
        {
            DeviceType = stringValue,
            OperatingSystem = stringValue,
            Browser = stringValue,
            AuthorizationType = stringValue
        };

    /// <summary>
    /// Get AuthorizationDataDomainModelNames with testing values 
    /// </summary>
    /// <returns>Array of strings</returns>
    internal static List<string> GetAuthorizationDataDomainModelNames()
    {
        var modelName = nameof(BaseVisitLogDomainModel.Authorization);

        return new List<string>
        {
            $"{modelName}.{nameof(AuthorizationDataDomainModel.DeviceType)}",
            $"{modelName}.{nameof(AuthorizationDataDomainModel.OperatingSystem)}",
            $"{modelName}.{nameof(AuthorizationDataDomainModel.Browser)}",
            $"{modelName}.{nameof(AuthorizationDataDomainModel.AuthorizationType)}"
        };
    }

    /// <summary>
    /// Get UserRoleDomainModel string params with testing values 
    /// </summary>
    /// <param name="stringValue">Testing string value</param>
    /// <returns>UserRoleDomainModel</returns>
    internal static UserRoleDomainModel GetUserRoleDomainStringModel(string stringValue) =>
        new(stringValue, stringValue);

    /// <summary>
    /// Get VisitLogConsumerMessage string params with testing values 
    /// </summary>
    /// <param name="stringValue">Testing string value</param>
    /// <returns>AuthorizationDataDomainModel</returns>
    internal static VisitLogConsumerMessage GetVisitLogConsumerStringMessage(string stringValue) =>
        new()
        {
            Login = stringValue,
            Ip = stringValue
        };

    /// <summary>
    /// Get VisitLogConsumerMessage Guid params with testing values 
    /// </summary>
    /// <param name="guidValue">Testing Guid value</param>
    /// <param name="logType">VisitLogType</param>
    /// <returns>VisitLogConsumerMessage</returns>
    internal static VisitLogConsumerMessage GetVisitLogConsumerGuidMessage(Guid guidValue, VisitLogType logType)
    {
        if (logType is VisitLogType.Player)
        {
            return new()
            {
                Type = logType,
                NodeId = guidValue,
                PlayerId = guidValue
            };
        }

        return new()
        {
            Type = logType,
            NodeId = guidValue,
            UserId = guidValue
        };
    }

    /// <summary>
    /// Get VisitLogConsumerMessage DateTime params with testing values 
    /// </summary>
    /// <param name="dateTimeValue">Testing DateTime value</param>
    /// <returns>VisitLogConsumerMessage</returns>
    internal static VisitLogConsumerMessage
        GetVisitLogTimeConsumerMessage(DateTime dateTimeValue) =>
        new()
        {
            Timestamp = dateTimeValue
        };

    /// <summary>
    /// Get VisitLogConsumerMessage Authorization params with testing values 
    /// </summary>
    /// <param name="authorizationValue">Testing Authorization values</param>
    /// <returns>VisitLogConsumerMessage</returns>
    internal static VisitLogConsumerMessage GetVisitLogAuthorizationConsumerMessage(
        AuthorizationDataDomainModel authorizationValue) =>
        new()
        {
            Authorization = authorizationValue
        };

    /// <summary>
    /// Get VisitLogConsumerMessage UserRoles params with testing values 
    /// </summary>
    /// <param name="userRoleValue">Testing UserRoles values</param>
    /// <param name="logType">VisitLogType</param>
    /// <returns>VisitLogConsumerMessage</returns>
    internal static VisitLogConsumerMessage GetVisitLogUserRolesConsumerMessage(
        List<UserRoleDomainModel>? userRoleValue, VisitLogType logType) =>
        new()
        {
            Type = logType,
            UserRoles = userRoleValue
        };

}