using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Tests.Extensions;
using AuditService.Tests.Fakes.Kafka.Validators;
using FluentValidation.TestHelper;
using KIT.Kafka.Consumers.VisitLog.Validators;

namespace AuditService.Tests.Tests.Kafka.Validators.VisitLog;

/// <summary>
/// AuthorizationDataDomainModelValidator tests
/// </summary>
public class AuthorizationDataDomainModelValidatorTest
{
    private readonly AuthorizationDataDomainModelValidator _validatorTest;

    public AuthorizationDataDomainModelValidatorTest()
    {
        var type = VisitLogType.Player;
        _validatorTest = new AuthorizationDataDomainModelValidator(type);
    }

    /// <summary>
    /// Testing string params for AuthorizationDataDomainModelValidator
    /// </summary>
    /// <param name="stringValue">String values for validation error testing</param>
    [Theory, InlineData(null), InlineData(""), InlineData(" ")]
    public void AuthorizationDataDomainModelValidator_InsertStringNotValidParams_ShouldHaveValidationError(string stringValue)
    {
        //Act
        var result = _validatorTest.TestValidate(VisitLogValidatorTestData.GetAuthorizationDataDomainStringModel(stringValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.DeviceType);
        result.ShouldHaveValidationErrorFor(log => log.OperatingSystem);
        result.ShouldHaveValidationErrorFor(log => log.Browser);
        result.ShouldHaveValidationErrorFor(log => log.OperatingSystem);

        ValidatorAsserts<AuthorizationDataDomainModel>.AssertDomainModelNames(result, VisitLogValidatorTestData.GetAuthorizationDataDomainModelNames());
    }
}