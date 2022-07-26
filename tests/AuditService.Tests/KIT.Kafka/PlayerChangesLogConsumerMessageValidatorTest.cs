using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using FluentValidation;
using FluentValidation.TestHelper;
using KIT.Kafka.Consumers.PlayerChangesLog;
using KIT.Kafka.Consumers.PlayerChangesLog.Validators;

namespace AuditService.Tests.KIT.Kafka
{
    public class PlayerChangesLogConsumerMessageValidatorTest
    {
        private PlayerChangesLogConsumerMessageValidator validatorTest;
        public PlayerChangesLogConsumerMessageValidatorTest()
        {
            var userValidatorTest = new UserInitiatorDomainModelValidator();
            var playerAttributeValidatorTest = new PlayerAttributeDomainModelValidator();
            validatorTest = new PlayerChangesLogConsumerMessageValidator(userValidatorTest, playerAttributeValidatorTest);

        }

        [Fact]
        public void Should_have_error_when_Name_is_null()
        {
            //Arrange
            var userValidator = new PlayerChangesLogConsumerMessage()
            {
                NodeId = Guid.Empty,
                ProjectId = Guid.Empty,
                EventCode = null,
                Timestamp = DateTime.Now,
                PlayerId = Guid.Empty,
                IpAddress = null,
                User = null,
                OldValues = null,
                NewValues = null
            };

            //Act
            var result = validatorTest.TestValidate(userValidator);
            result.ShouldHaveValidationErrorFor(log => log.NodeId);
        }
    }
}
