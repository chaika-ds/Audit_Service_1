using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Tests.KIT.Kafka.Asserts;
using AuditService.Tests.KIT.Kafka.TestData;
using FluentValidation.TestHelper;
using KIT.Kafka.Consumers.PlayerChangesLog.Validators;

namespace AuditService.Tests.KIT.Kafka.ValidatorTests;

/// <summary>
/// UserInitiatorDomainModelValidatorTest tests
/// </summary>
public class UserInitiatorDomainModelValidatorTest
{
    private readonly UserInitiatorDomainModelValidator _userValidatorTest;

    public UserInitiatorDomainModelValidatorTest()
    {
        _userValidatorTest = new UserInitiatorDomainModelValidator();

    }

    /// <summary>
    /// Testing string params for PlayerChangesLogConsumerMessageValidator
    /// </summary>
    /// <param name="stringValue">String values for validation error testing</param>
    [Theory, InlineData(null), InlineData(""), InlineData(" ")]
    public void UserInitiatorDomainModelValidator_InsertStringNotValidParams_ShouldHaveValidationError(
        string stringValue)
    {
        //Act
        var result = _userValidatorTest.TestValidate(PlayerChangesLogValidatorTestData
            .GetUserInitiatorDomainModelLogStringConsumerMessage(stringValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.Email);
        result.ShouldHaveValidationErrorFor(log => log.UserAgent);
        ValidatorAsserts<UserInitiatorDomainModel>.AssertDomainModelNames(result, PlayerChangesLogValidatorTestData.GetUserInitiatorDomainModelNames());
    }

    /// <summary>
    /// Testing Guid params for PlayerChangesLogConsumerMessageValidator
    /// </summary>
    /// <param name="guidValue">Guid values for validation error testing</param>
    [Theory, MemberData(nameof(GetUserInitiatorDomainModelValidatorGuidValues))]
    public void UserInitiatorDomainModelValidator_InsertGuidNotValidParams_ShouldHaveValidationError(
        Guid guidValue)
    {
        //Act
        var result = _userValidatorTest.TestValidate(PlayerChangesLogValidatorTestData
            .GetUserInitiatorDomainModelGuidConsumerMessage(guidValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.Id);
    }

    /// <summary>
    /// Guid values for testing
    /// </summary>
    public static IEnumerable<object[]> GetUserInitiatorDomainModelValidatorGuidValues
    {
        get
        {
            yield return new object[] { Guid.Empty };
            yield return new object[] { new Guid() };
        }
    }
}