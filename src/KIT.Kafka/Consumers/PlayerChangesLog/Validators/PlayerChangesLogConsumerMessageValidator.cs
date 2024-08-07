﻿using AuditService.Common.Models.Domain.PlayerChangesLog;
using FluentValidation;

namespace KIT.Kafka.Consumers.PlayerChangesLog.Validators;

/// <summary>
///     Player changes log consumer message model validator
/// </summary>
public class PlayerChangesLogConsumerMessageValidator : AbstractValidator<PlayerChangesLogConsumerMessage>
{
    public PlayerChangesLogConsumerMessageValidator(IValidator<UserInitiatorDomainModel> userValidator,
        IValidator<PlayerAttributeDomainModel> playerAttributeValidator)
    {
        RuleFor(message => message.NodeId).NotEmpty();
        RuleFor(message => message.EventCode).NotEmpty();
        RuleFor(message => message.Timestamp).NotEmpty();
        RuleFor(message => message.PlayerId).NotEmpty();
        RuleFor(message => message.IpAddress).NotEmpty();
        RuleFor(message => message.User).NotNull().SetValidator(userValidator);
        RuleFor(message => message.OldValue).NotEmpty();
        RuleFor(message => message.NewValue).NotEmpty();
        RuleForEach(message => message.OldValue).SetValidator(playerAttributeValidator);
        RuleForEach(message => message.NewValue).SetValidator(playerAttributeValidator);
    }
}