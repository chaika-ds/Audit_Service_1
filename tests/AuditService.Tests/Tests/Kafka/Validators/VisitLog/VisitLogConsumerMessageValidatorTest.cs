using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Tests.Fakes.Kafka.Validators;
using FluentValidation;
using FluentValidation.TestHelper;
using KIT.Kafka.Consumers.VisitLog.Validators;

namespace AuditService.Tests.Tests.Kafka.Validators.VisitLog;

/// <summary>
/// VisitLogConsumerMessageValidator tests
/// </summary>
public class VisitLogConsumerMessageValidatorTest
{
    private readonly VisitLogConsumerMessageValidator _validatorTest;

    public VisitLogConsumerMessageValidatorTest()
    {
        var userRoleDomainModelValidatorTest = new InlineValidator<UserRoleDomainModel>();
        _validatorTest = new VisitLogConsumerMessageValidator(userRoleDomainModelValidatorTest);
    }

    /// <summary>
    /// Testing string params for VisitLogConsumerMessageValidator
    /// </summary>
    /// <param name="stringValue">String values for validation error testing</param>
    [Theory, InlineData(null), InlineData(""), InlineData(" ")]
    public void VisitLogConsumerMessageValidator_InsertStringNotValidParams_ShouldHaveValidationError(string stringValue)
    {
        //Act
        var result = _validatorTest.TestValidate(VisitLogValidatorTestData.GetVisitLogConsumerStringMessage(stringValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.Login);
        result.ShouldHaveValidationErrorFor(log => log.Ip);
    }

    /// <summary>
    /// Testing Guid params for VisitLogConsumerMessageValidator for type Player
    /// </summary>
    /// <param name="guidValue">Guid values for validation error testing</param>
    [Theory, MemberData(nameof(ValidatorDataGenerator.GetTestGuidValues), MemberType = typeof(ValidatorDataGenerator))]
    public void VisitLogConsumerMessageValidator_InsertGuidNotValidParamsWithTypePlayer_ShouldHaveValidationError(Guid guidValue)
    {
        //Arrange
        var visitLogType = VisitLogType.Player;

        //Act
        var result = _validatorTest.TestValidate(VisitLogValidatorTestData.GetVisitLogConsumerGuidMessage(guidValue, visitLogType));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.NodeId);
        result.ShouldHaveValidationErrorFor(log => log.PlayerId);
    }

    /// <summary>
    /// Testing Guid params for VisitLogConsumerMessageValidator for type User
    /// </summary>
    /// <param name="guidValue">Guid values for validation error testing</param>
    [Theory, MemberData(nameof(ValidatorDataGenerator.GetTestGuidValues), MemberType = typeof(ValidatorDataGenerator))]
    public void VisitLogConsumerMessageValidator_InsertGuidNotValidParamsWithTypeUser_ShouldHaveValidationError(Guid guidValue)
    {
        //Arrange
        var visitLogType = VisitLogType.User;

        //Act
        var result = _validatorTest.TestValidate(VisitLogValidatorTestData.GetVisitLogConsumerGuidMessage(guidValue, visitLogType));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.NodeId);
        result.ShouldHaveValidationErrorFor(log => log.UserId);
    }

    /// <summary>
    /// Testing DateTime params for VisitLogConsumerMessageValidator
    /// </summary>
    /// <param name="dateTimeValue">DateTime values for validation error testing</param>
    [Theory, MemberData(nameof(ValidatorDataGenerator.GetTestDateTimeValues), MemberType = typeof(ValidatorDataGenerator))]
    public void VisitLogConsumerMessageValidator_InsertDateTimeNotValidParams_ShouldHaveValidationError(DateTime dateTimeValue)
    {
        //Act
        var result = _validatorTest.TestValidate(VisitLogValidatorTestData.GetVisitLogTimeConsumerMessage(dateTimeValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.Timestamp);
    }

    /// <summary>
    /// Testing Authorization params for VisitLogConsumerMessageValidator
    /// </summary>
    /// <param name="authorizationValue">Authorization values for validation error testing</param>
    [Theory, MemberData(nameof(ValidatorDataGenerator.GetAuthorizationDataDomainModelValues), MemberType = typeof(ValidatorDataGenerator))]
    public void VisitLogConsumerMessageValidator_InsertAuthorizationNotValidParams_ShouldHaveValidationError(AuthorizationDataDomainModel authorizationValue)
    {
        //Act
        var result = _validatorTest.TestValidate(VisitLogValidatorTestData.GetVisitLogAuthorizationConsumerMessage(authorizationValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.Authorization);
    }

    /// <summary>
    /// Testing UserRoles params for VisitLogConsumerMessageValidator
    /// </summary>
    /// <param name="userRoleValue">UserRoles values for validation error testing</param>
    [Theory, MemberData(nameof(ValidatorDataGenerator.GetListOfUserRoleDomainModelValues), MemberType = typeof(ValidatorDataGenerator))]
    public void VisitLogConsumerMessageValidator_InsertUserNotValidParams_ShouldHaveValidationError(List<UserRoleDomainModel>? userRoleValue)
    {
        //Arrange
        var visitLogType = VisitLogType.User;

        //Act
        var result = _validatorTest.TestValidate(VisitLogValidatorTestData.GetVisitLogUserRolesConsumerMessage(userRoleValue, visitLogType));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.UserRoles);
    }
}