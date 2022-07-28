using AuditService.Common.Models.Domain.PlayerChangesLog;
using FluentValidation;

namespace KIT.Kafka.Consumers.PlayerChangesLog.Validators;

/// <summary>
///     Player attribute model validator
/// </summary>
public class PlayerAttributeDomainModelValidator : AbstractValidator<PlayerAttributeDomainModel>
{
    public PlayerAttributeDomainModelValidator()
    {
        RuleFor(playerAttribute => playerAttribute.Value).NotEmpty();
        RuleFor(playerAttribute => playerAttribute.Type).NotEmpty();
    }
}