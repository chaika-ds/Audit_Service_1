using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Tests.KIT.Kafka.TestModels;
using FluentValidation;
using FluentValidation.TestHelper;
using KIT.Kafka.Consumers.PlayerChangesLog;
using KIT.Kafka.Consumers.PlayerChangesLog.Validators;

namespace AuditService.Tests.KIT.Kafka
{
    public class PlayerChangesLogValidatorTest
    {
        private UserInitiatorDomainModelValidator userValidatorTest;
        private PlayerAttributeDomainModelValidator playerAttributeValidatorTest;
        private PlayerChangesLogConsumerMessageValidator validatorTest;
        public PlayerChangesLogValidatorTest()
        {
            var userValidatorTest = new UserInitiatorDomainModelValidator();
            var playerAttributeValidatorTest = new PlayerAttributeDomainModelValidator();
            validatorTest = new PlayerChangesLogConsumerMessageValidator(userValidatorTest, playerAttributeValidatorTest);
        }

        [Fact]
        public void PlayerChangesLogConsumerMessageValidator_InsertNotValidParams_ShouldHaveValidationError()
        {
            //Act
            var result = validatorTest.TestValidate(PlayerChangesLogValidatorTestModels.GetPlayerChangesLogConsumerMessage());

            //Assert
            result.ShouldHaveValidationErrorFor(log => log.NodeId);
            result.ShouldHaveValidationErrorFor(log => log.ProjectId);
            result.ShouldHaveValidationErrorFor(log => log.EventCode);
            result.ShouldHaveValidationErrorFor(log => log.Timestamp);
            result.ShouldHaveValidationErrorFor(log => log.PlayerId);
            result.ShouldHaveValidationErrorFor(log => log.IpAddress);
            result.ShouldHaveValidationErrorFor(log => log.User);
        }

        [Fact]
        public void UserInitiatorDomainModelValidator_InsertNotValidParams_ShouldHaveValidationError()
        {
            //Act
            var result = userValidatorTest.TestValidate(PlayerChangesLogValidatorTestModels.GetUserInitiatorDomainModel());

            //Assert
            result.ShouldHaveValidationErrorFor(log => log.Email);
            result.ShouldHaveValidationErrorFor(log => log.Id);
            result.ShouldHaveValidationErrorFor(log => log.UserAgent);
        }
    }
}
