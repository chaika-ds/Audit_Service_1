using AuditService.Common.Models.Domain.AuditLog;
using AuditService.Tests.Extensions;
using AuditService.Tests.Fakes.Kafka.Validators;
using FluentValidation.TestHelper;
using KIT.Kafka.Consumers.AuditLog.Validators;

namespace AuditService.Tests.Tests.Kafka.Validators.AuditLog;

/// <summary>
/// IdentityUserDomainModelValidator tests
/// </summary>
public class IdentityUserDomainModelValidatorTest
{
    private readonly IdentityUserDomainModelValidator _validatorTest;

    public IdentityUserDomainModelValidatorTest()
    {
        _validatorTest = new IdentityUserDomainModelValidator();
    }

    /// <summary>
    /// Testing string params for IdentityUserDomainModelValidator
    /// </summary>
    /// <param name="stringValue">String values for validation error testing</param>
    [Theory, InlineData(null), InlineData(""), InlineData(" ")]
    public void IdentityUserDomainModelValidator_InsertStringNotValidParams_ShouldHaveValidationError(
        string stringValue)
    {
        //Act
        var result =
            _validatorTest.TestValidate(AuditLogValidatorTestData.GetIdentityUserDomainStringModel(stringValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.Ip);
        result.ShouldHaveValidationErrorFor(log => log.Login);
        result.ShouldHaveValidationErrorFor(log => log.UserAgent);
        ValidatorAsserts<AuditLogUserDomainModel>.AssertDomainModelNames(result, PlayerChangesLogValidatorTestData.GetUserInitiatorDomainModelNames());

    }

    /// <summary>
    /// Testing Guid params for IdentityUserDomainModelValidator
    /// </summary>
    /// <param name="guidValue">Guid values for validation error testing</param>
    [Theory, MemberData(nameof(ValidatorDataGenerator.GetTestGuidValues), MemberType = typeof(ValidatorDataGenerator))]
    public void IdentityUserDomainModelValidator_InsertGuidNotValidParamsWithTypePlayer_ShouldHaveValidationError(
        Guid guidValue)
    {
        //Act
        var result = _validatorTest.TestValidate(AuditLogValidatorTestData.GetIdentityUserDomainGuidModel(guidValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.Id);
        ValidatorAsserts<AuditLogUserDomainModel>.AssertDomainModelNames(result, AuditLogValidatorTestData.GetIdentityUserDomainModelIpName());
    }
}