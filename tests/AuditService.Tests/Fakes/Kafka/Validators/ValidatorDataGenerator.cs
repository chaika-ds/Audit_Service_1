using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using DocumentFormat.OpenXml.Office2010.PowerPoint;

namespace AuditService.Tests.Fakes.Kafka.Validators;

public class ValidatorDataGenerator
{
    /// <summary>
    /// Guid values for testing
    /// </summary>
    public static IEnumerable<object[]> GetTestGuidValues
    {
        get
        {
            yield return new object[] { Guid.Empty };
            yield return new object[] { new Guid() };
        }
    }

    /// <summary>
    /// DateTime values for testing
    /// </summary>
    public static IEnumerable<object[]> GetTestDateTimeValues
    {
        get
        {
            yield return new object[] { DateTime.MinValue };
            yield return new object[] { null! };
            yield return new object[] { default(DateTime) };
        }
    }

    /// <summary>
    /// IdentityUserDomainModel values for testing
    /// </summary>
    public static IEnumerable<object[]> GetIdentityUserDomainModelValues
    {
        get
        {
            yield return new object[] { null! };
            yield return new object[] { default(IdentityUserDomainModel)! };
        }
    }

    /// <summary>
    /// UserInitiatorDomainModel values for testing
    /// </summary>
    public static IEnumerable<object[]> GetUserInitiatorDomainModelValues
    {
        get
        {
            yield return new object[] { null! };
            yield return new object[] { default(UserInitiatorDomainModel)! };
        }
    }

    /// <summary>
    /// PlayerAttributeDomainModel Dictionary values for testing
    /// </summary>
    public static IEnumerable<object[]> GetPlayerAttributeDomainModelDictionaryValues
    {
        get { yield return new object[] { new Dictionary<string, PlayerAttributeDomainModel>() }; }
    }

    /// <summary>
    /// AuthorizationDataDomainModel values for testing
    /// </summary>
    public static IEnumerable<object[]> GetAuthorizationDataDomainModelValues
    {
        get
        {
            yield return new object[] { null! };
            yield return new object[] { default(AuthorizationDataDomainModel)! };
        }
    }

    /// <summary>
    /// List of UserRoleDomainModel values for testing
    /// </summary>
    public static IEnumerable<object[]> GetListOfUserRoleDomainModelValues
    {
        get
        {
            yield return new object[] { null! };
            yield return new object[] { default(List<UserRoleDomainModel>?)! };
            yield return new object[] { new List<UserRoleDomainModel>() };
        }
    }
}