using AuditService.Common.Models.Domain.PlayerChangesLog;
using FluentValidation;

namespace KIT.Kafka.Consumers.PlayerChangesLog.Validators;

/// <summary>
/// Event initiator model validator
/// </summary>
public class UserInitiatorDomainModelValidator : AbstractValidator<UserInitiatorDomainModel>
{
    public UserInitiatorDomainModelValidator()
    {
        RuleFor(user => user.Id).NotEmpty().WithName($"User.{nameof(UserInitiatorDomainModel.Id)}");
        RuleFor(user => user.Email).NotEmpty().WithName($"User.{nameof(UserInitiatorDomainModel.Email)}");
        RuleFor(user => user.UserAgent).NotEmpty().WithName($"User.{nameof(UserInitiatorDomainModel.UserAgent)}");
    }
}