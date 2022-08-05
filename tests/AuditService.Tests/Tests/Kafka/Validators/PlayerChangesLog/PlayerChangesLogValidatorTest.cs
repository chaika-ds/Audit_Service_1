using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Tests.Fakes.Kafka.Validators;
using FluentValidation.TestHelper;
using KIT.Kafka.Consumers.PlayerChangesLog.Validators;

namespace AuditService.Tests.Tests.Kafka.Validators.PlayerChangesLog;

/// <summary>
/// PlayerChangesLogConsumerMessageValidator tests
/// </summary>
public class PlayerChangesLogValidatorTest
{
    private readonly PlayerChangesLogConsumerMessageValidator _validatorTest;

    public PlayerChangesLogValidatorTest()
    {
        var userValidatorTest = new UserInitiatorDomainModelValidator();
        var playerAttributeValidatorTest = new PlayerAttributeDomainModelValidator();
        _validatorTest = new PlayerChangesLogConsumerMessageValidator(userValidatorTest, playerAttributeValidatorTest);
    }

    /// <summary>
    /// Testing string params for PlayerChangesLogConsumerMessageValidator
    /// </summary>
    /// <param name="stringValue">String values for validation error testing</param>
    [Theory, InlineData(null), InlineData(""), InlineData(" ")]
    public void PlayerChangesLogConsumerMessageValidator_InsertStringNotValidParams_ShouldHaveValidationError(string stringValue)
    {
        //Act
        var result = _validatorTest.TestValidate(PlayerChangesLogValidatorTestData.GetPlayerChangesLogStringConsumerMessage(stringValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.EventCode);
        result.ShouldHaveValidationErrorFor(log => log.IpAddress);
    }

    /// <summary>
    /// Testing Guid params for PlayerChangesLogConsumerMessageValidator
    /// </summary>
    /// <param name="guidValue">Guid values for validation error testing</param>
    [Theory, MemberData(nameof(ValidatorDataGenerator.GetTestGuidValues), MemberType =typeof(ValidatorDataGenerator))]
    public void PlayerChangesLogConsumerMessageValidator_InsertGuidNotValidParams_ShouldHaveValidationError(Guid guidValue)
    {
        //Act
        var result = _validatorTest.TestValidate(PlayerChangesLogValidatorTestData.GetPlayerChangesLogGuidConsumerMessage(guidValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.NodeId);
        result.ShouldHaveValidationErrorFor(log => log.ProjectId);
        result.ShouldHaveValidationErrorFor(log => log.PlayerId);
    }

    /// <summary>
    /// Testing DateTime params for PlayerChangesLogConsumerMessageValidator
    /// </summary>
    /// <param name="dateTimeValue">DateTime values for validation error testing</param>
    [Theory, MemberData(nameof(ValidatorDataGenerator.GetTestDateTimeValues), MemberType = typeof(ValidatorDataGenerator))]
    public void PlayerChangesLogConsumerMessageValidator_InsertDateTimeNotValidParams_ShouldHaveValidationError(DateTime dateTimeValue)
    {
        //Act
        var result = _validatorTest.TestValidate(PlayerChangesLogValidatorTestData.GetPlayerChangesLogDateTimeConsumerMessage(dateTimeValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.Timestamp);
    }

    /// <summary>
    /// Testing Old-NewValue params for PlayerChangesLogConsumerMessageValidator
    /// </summary>
    /// <param name="value">Old-NewValues values for validation error testing</param>
    [Theory, MemberData(nameof(ValidatorDataGenerator.GetPlayerAttributeDomainModelDictionaryValues), MemberType = typeof(ValidatorDataGenerator))]
    public void PlayerChangesLogConsumerMessageValidator_InsertOldNewValueNotValidParams_ShouldHaveValidationError(Dictionary<string, PlayerAttributeDomainModel> value)
    {
        //Act
        var result = _validatorTest.TestValidate(PlayerChangesLogValidatorTestData.GetPlayerChangesLogOldNewValueConsumerMessage(value));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.OldValues);
        result.ShouldHaveValidationErrorFor(log => log.NewValues);
    }

    /// <summary>
    /// Testing User params for PlayerChangesLogConsumerMessageValidator
    /// </summary>
    /// <param name="userValue">User values for validation error testing</param>
    [Theory, MemberData(nameof(ValidatorDataGenerator.GetUserInitiatorDomainModelValues), MemberType = typeof(ValidatorDataGenerator))]
    public void PlayerChangesLogConsumerMessageValidator_InsertUserNotValidParams_ShouldHaveValidationError(UserInitiatorDomainModel userValue)
    {
        //Act
        var result = _validatorTest.TestValidate(PlayerChangesLogValidatorTestData.GetPlayerChangesLogUserConsumerMessage(userValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.User);
    }
}