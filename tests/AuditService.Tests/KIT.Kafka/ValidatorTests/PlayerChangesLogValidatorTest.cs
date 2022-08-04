using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Tests.KIT.Kafka.TestData;
using FluentValidation.TestHelper;
using KIT.Kafka.Consumers.PlayerChangesLog.Validators;

namespace AuditService.Tests.KIT.Kafka.ValidatorTests;

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
    [Theory, MemberData(nameof(GetPlayerChangesLogConsumerMessageGuidValues))]
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
    /// Guid values for testing
    /// </summary>
    public static IEnumerable<object[]> GetPlayerChangesLogConsumerMessageGuidValues
    {
        get
        {
            yield return new object[] { Guid.Empty };
            yield return new object[] { new Guid() };
        }
    }

    /// <summary>
    /// Testing DateTime params for PlayerChangesLogConsumerMessageValidator
    /// </summary>
    /// <param name="dateTimeValue">DateTime values for validation error testing</param>
    [Theory, MemberData(nameof(GetPlayerChangesLogConsumerMessageDateTimeValues))]
    public void PlayerChangesLogConsumerMessageValidator_InsertDateTimeNotValidParams_ShouldHaveValidationError(DateTime dateTimeValue)
    {
        //Act
        var result = _validatorTest.TestValidate(PlayerChangesLogValidatorTestData.GetPlayerChangesLogDateTimeConsumerMessage(dateTimeValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.Timestamp);
    }

    /// <summary>
    /// DateTime values for testing
    /// </summary>
    public static IEnumerable<object[]> GetPlayerChangesLogConsumerMessageDateTimeValues
    {
        get
        {
            yield return new object[] { DateTime.MinValue };
            yield return new object[] { null! };
            yield return new object[] { default(DateTime) };

        }
    }

    /// <summary>
    /// Testing Old-NewValue params for PlayerChangesLogConsumerMessageValidator
    /// </summary>
    /// <param name="value">Old-NewValues values for validation error testing</param>
    [Theory, MemberData(nameof(GetPlayerChangesLogConsumerMessageOldNewValueValues))]
    public void PlayerChangesLogConsumerMessageValidator_InsertOldNewValueNotValidParams_ShouldHaveValidationError(Dictionary<string, PlayerAttributeDomainModel> value)
    {
        //Act
        var result = _validatorTest.TestValidate(PlayerChangesLogValidatorTestData.GetPlayerChangesLogOldNewValueConsumerMessage(value));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.OldValues);
        result.ShouldHaveValidationErrorFor(log => log.NewValues);
    }

    /// <summary>
    /// Old-NewValues values for testing
    /// </summary>
    public static IEnumerable<object[]> GetPlayerChangesLogConsumerMessageOldNewValueValues
    {
        get
        {
            yield return new object[] { new Dictionary<string, PlayerAttributeDomainModel>() };
        }
    }

    /// <summary>
    /// Testing User params for PlayerChangesLogConsumerMessageValidator
    /// </summary>
    /// <param name="userValue">User values for validation error testing</param>
    [Theory, MemberData(nameof(GetPlayerChangesLogConsumerMessageUserValues))]
    public void PlayerChangesLogConsumerMessageValidator_InsertUserNotValidParams_ShouldHaveValidationError(UserInitiatorDomainModel userValue)
    {
        //Act
        var result = _validatorTest.TestValidate(PlayerChangesLogValidatorTestData.GetPlayerChangesLogUserConsumerMessage(userValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.User);
    }

    /// <summary>
    /// User values for testing
    /// </summary>
    public static IEnumerable<object[]> GetPlayerChangesLogConsumerMessageUserValues
    {
        get
        {
            yield return new object[] { null! };
            yield return new object[] { default(UserInitiatorDomainModel)! };
        }
    }
}