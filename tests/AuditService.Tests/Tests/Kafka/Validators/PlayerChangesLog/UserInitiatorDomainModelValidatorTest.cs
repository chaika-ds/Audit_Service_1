﻿using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Tests.Extensions;
using AuditService.Tests.Fakes.Kafka.Validators;
using FluentValidation.TestHelper;
using KIT.Kafka.Consumers.PlayerChangesLog.Validators;

namespace AuditService.Tests.Tests.Kafka.Validators.PlayerChangesLog;

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
    [Theory, MemberData(nameof(ValidatorDataGenerator.GetTestGuidValues), MemberType = typeof(ValidatorDataGenerator))]
    public void UserInitiatorDomainModelValidator_InsertGuidNotValidParams_ShouldHaveValidationError(
        Guid guidValue)
    {
        //Act
        var result = _userValidatorTest.TestValidate(PlayerChangesLogValidatorTestData
            .GetUserInitiatorDomainModelGuidConsumerMessage(guidValue));

        //Assert
        result.ShouldHaveValidationErrorFor(log => log.Id);
    }
}