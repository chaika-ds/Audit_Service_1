using AuditService.Common.Models.Domain;
using AuditService.Tests.Fakes.Kafka.Validators;
using FluentValidation.TestHelper;
using KIT.Kafka.Consumers.AuditLog.Validators;

namespace AuditService.Tests.Tests.Kafka.Validators.AuditLog;

/// <summary>
/// AuditLogConsumerMessageValidator tests
/// </summary>
public class AuditLogConsumerMessageValidatorTest
{
    private readonly AuditLogConsumerMessageValidator _validatorTest;

    public AuditLogConsumerMessageValidatorTest()
    {
        var userValidatorTest = new IdentityUserDomainModelValidator();
        _validatorTest = new AuditLogConsumerMessageValidator(userValidatorTest);
    }

    /// <summary>
    /// Testing string params for AuditLogConsumerMessageValidator
    /// </summary>
    /// <param name="stringValue">String values for validation error testing</param>
    [Theory, InlineData(null), InlineData(""), InlineData(" ")]
    public void AuditLogConsumerMessageValidator_InsertStringNotValidParams_ShouldHaveValidationError(string stringValue)
    {
        //Act
        var result = _validatorTest.TestValidate(AuditLogValidatorTestData.GetAuditLogConsumerStringMessage(stringValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.CategoryCode);
    }

    /// <summary>
    /// Testing Guid params for AuditLogConsumerMessageValidator
    /// </summary>
    /// <param name="guidValue">Guid values for validation error testing</param>
    [Theory, MemberData(nameof(ValidatorDataGenerator.GetTestGuidValues), MemberType = typeof(ValidatorDataGenerator))]
    public void AuditLogConsumerMessageValidator_InsertGuidNotValidParamsWithTypePlayer_ShouldHaveValidationError(Guid guidValue)
    {
        //Act
        var result = _validatorTest.TestValidate(AuditLogValidatorTestData.GetAuditLogConsumerGuidMessage(guidValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.NodeId);
    }

    /// <summary>
    /// Testing DateTime params for AuditLogConsumerMessageValidator
    /// </summary>
    /// <param name="dateTimeValue">DateTime values for validation error testing</param>
    [Theory, MemberData(nameof(ValidatorDataGenerator.GetTestDateTimeValues), MemberType = typeof(ValidatorDataGenerator))]
    public void AuditLogConsumerMessageValidator_InsertDateTimeNotValidParams_ShouldHaveValidationError(DateTime dateTimeValue)
    {
        //Act
        var result = _validatorTest.TestValidate(AuditLogValidatorTestData.GetAuditLogDateTimeConsumerMessage(dateTimeValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.Timestamp);
    }

    /// <summary>
    /// Testing User params for AuditLogConsumerMessageValidator
    /// </summary>
    /// <param name="userRoleValue">UserRoles values for validation error testing</param>
    [Theory, MemberData(nameof(ValidatorDataGenerator.GetIdentityUserDomainModelValues), MemberType = typeof(ValidatorDataGenerator))]
    public void AuditLogConsumerMessageValidator_InsertUserNotValidParams_ShouldHaveValidationError(IdentityUserDomainModel userRoleValue)
    {
        //Act
        var result = _validatorTest.TestValidate(AuditLogValidatorTestData.GetVisitLogUserConsumerMessage(userRoleValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.User);
    }
}