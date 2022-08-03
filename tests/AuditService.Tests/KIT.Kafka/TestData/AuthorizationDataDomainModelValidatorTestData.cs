using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.VisitLog;

namespace AuditService.Tests.KIT.Kafka.TestData;

/// <summary>
/// Data for testing AuthorizationDataDomainModelValidator
/// </summary>
internal class AuthorizationDataDomainModelValidatorTestData
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
}