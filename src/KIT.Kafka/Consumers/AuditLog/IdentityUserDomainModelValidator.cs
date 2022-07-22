using AuditService.Common.Models.Domain;
using FluentValidation;

namespace KIT.Kafka.Consumers.AuditLog;

/// <summary>
/// User information model validator.
/// </summary>
public class IdentityUserDomainModelValidator : AbstractValidator<IdentityUserDomainModel>
{
    public IdentityUserDomainModelValidator()
    {
        RuleFor(user => user.Id).NotEmpty().WithName($"User.{nameof(IdentityUserDomainModel.Id)}");
        RuleFor(user => user.Ip).NotEmpty().WithName($"User.{nameof(IdentityUserDomainModel.Ip)}");
        RuleFor(user => user.Email).NotEmpty().WithName($"User.{nameof(IdentityUserDomainModel.Email)}");
        RuleFor(user => user.UserAgent).NotEmpty().WithName($"User.{nameof(IdentityUserDomainModel.UserAgent)}");
    }
}