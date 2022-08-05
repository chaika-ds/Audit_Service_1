using AuditService.Tests.Fakes.Kafka.Validators;
using FluentValidation.TestHelper;
using KIT.Kafka.Consumers.BlockedPlayersLog.Validators;

namespace AuditService.Tests.Tests.Kafka.Validators;

/// <summary>
/// BlockedPlayersLogConsumerMessageValidator tests
/// </summary>
public class BlockedPlayersLogConsumerMessageValidatorTest
{
    private readonly BlockedPlayersLogConsumerMessageValidator _validatorTest;

    public BlockedPlayersLogConsumerMessageValidatorTest() {
           
        _validatorTest = new BlockedPlayersLogConsumerMessageValidator();
    }

    /// <summary>
    /// Testing string params for BlockedPlayersLogConsumerMessageValidator
    /// </summary>
    /// <param name="stringValue">String values for validation error testing</param>
    [Theory, InlineData(null), InlineData(""), InlineData(" ")]
    public void BlockedPlayersLogConsumerMessageValidator_InsertStringNotValidParams_ShouldHaveValidationError(string stringValue)
    {
        //Act
        var result = _validatorTest.TestValidate(BlockedPlayersLogConsumerMessageValidatorTestData.GetBlockedPlayersLogStringConsumerMessage(stringValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.HallName);
        result.ShouldHaveValidationErrorFor(log => log.ProjectName);
        result.ShouldHaveValidationErrorFor(log => log.LastVisitIpAddress);
        result.ShouldHaveValidationErrorFor(log => log.Platform);
        result.ShouldHaveValidationErrorFor(log => log.PlayerLogin);
        result.ShouldHaveValidationErrorFor(log => log.Browser);
        result.ShouldHaveValidationErrorFor(log => log.BrowserVersion);
        result.ShouldHaveValidationErrorFor(log => log.Language);
    }

    /// <summary>
    /// Testing Guid params for BlockedPlayersLogConsumerMessageValidator
    /// </summary>
    /// <param name="guidValue">Guid values for validation error testing</param>
    [Theory, MemberData(nameof(GetBlockedPlayersLogConsumerMessageGuidValues))]
    public void BlockedPlayersLogConsumerMessageValidator_InsertGuidNotValidParams_ShouldHaveValidationError(Guid guidValue)
    {
        //Act
        var result = _validatorTest.TestValidate(BlockedPlayersLogConsumerMessageValidatorTestData.GetBlockedPlayersLogGuidConsumerMessage(guidValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.ProjectId);
        result.ShouldHaveValidationErrorFor(log => log.HallId);
        result.ShouldHaveValidationErrorFor(log => log.PlayerId);
    }

    /// <summary>
    /// Guid values for testing
    /// </summary>
    public static IEnumerable<object[]> GetBlockedPlayersLogConsumerMessageGuidValues
    {
        get
        {
            yield return new object[] { Guid.Empty };
            yield return new object[] { new Guid() };
        }
    }

    /// <summary>
    /// Testing DateTime params for BlockedPlayersLogConsumerMessageValidator
    /// </summary>
    /// <param name="dateTimeValue">DateTime values for validation error testing</param>
    [Theory, MemberData(nameof(GetBlockedPlayersLogConsumerMessageDateTimeValues))]
    public void BlockedPlayersLogConsumerMessageValidator_InsertDateTimeNotValidParams_ShouldHaveValidationError(DateTime dateTimeValue)
    {
        //Act
        var result = _validatorTest.TestValidate(BlockedPlayersLogConsumerMessageValidatorTestData.GetBlockedPlayersLogDateTimeConsumerMessage(dateTimeValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.BlockingDate);
    }

    /// <summary>
    /// DateTime values for testing
    /// </summary>
    public static IEnumerable<object[]> GetBlockedPlayersLogConsumerMessageDateTimeValues
    {
        get
        {
            yield return new object[] { DateTime.MinValue };
            yield return new object[] { null! };
            yield return new object[] { default(DateTime) };
        }
    }
}