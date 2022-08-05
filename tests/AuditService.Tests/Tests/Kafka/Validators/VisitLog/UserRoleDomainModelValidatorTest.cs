using AuditService.Tests.Fakes.Kafka.Validators;
using FluentValidation.TestHelper;
using KIT.Kafka.Consumers.VisitLog.Validators;

namespace AuditService.Tests.Tests.Kafka.Validators.VisitLog;

public class UserRoleDomainModelValidatorTest
{
    private readonly UserRoleDomainModelValidator _validatorTest;

    public UserRoleDomainModelValidatorTest()
    {
        _validatorTest = new UserRoleDomainModelValidator();
    }

    /// <summary>
    /// Testing string params for UserRoleDomainModelValidator
    /// </summary>
    /// <param name="stringValue">String values for validation error testing</param>
    [Theory, InlineData(null), InlineData(""), InlineData(" ")]
    public void UserRoleDomainModelValidator_InsertStringNotValidParams_ShouldHaveValidationError(string stringValue)
    {
        //Act
        var result = _validatorTest.TestValidate(VisitLogValidatorTestData.GetUserRoleDomainStringModel(stringValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.Name);
        result.ShouldHaveValidationErrorFor(log => log.Code);
    }
}