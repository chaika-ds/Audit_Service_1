using AuditService.Tests.Fakes.Kafka.Validators;
using FluentValidation.TestHelper;
using KIT.Kafka.Consumers.PlayerChangesLog.Validators;

namespace AuditService.Tests.Tests.Kafka.Validators.PlayerChangesLog;

public class PlayerAttributeDomainModelValidatorTest
{
    private readonly PlayerAttributeDomainModelValidator _playerAttributeValidatorTest;

    public PlayerAttributeDomainModelValidatorTest()
    {
        _playerAttributeValidatorTest = new PlayerAttributeDomainModelValidator();
    }

    /// <summary>
    /// Testing string params for PlayerAttributeDomainModelValidator
    /// </summary>
    /// <param name="stringValue">String values for validation error testing</param>
    [Theory, InlineData(null), InlineData(""), InlineData(" ")]
    public void PlayerAttributeDomainModelValidator_InsertStringNotValidParams_ShouldHaveValidationError(
        string stringValue)
    {
        //Act
        var result = _playerAttributeValidatorTest.TestValidate(PlayerChangesLogValidatorTestData
            .GetPlayerAttributeDomainModel(stringValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.Value);
        result.ShouldHaveValidationErrorFor(log => log.Type);
    }
}