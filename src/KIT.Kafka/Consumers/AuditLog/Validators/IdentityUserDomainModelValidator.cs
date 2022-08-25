using AuditService.Common.Models.Domain.AuditLog;
using FluentValidation;

namespace KIT.Kafka.Consumers.AuditLog.Validators;

/// <summary>
/// User information model validator.
/// </summary>
public class IdentityUserDomainModelValidator : AbstractValidator<AuditLogUserDomainModel>
{
    public IdentityUserDomainModelValidator()
    {
        RuleFor(user => user.Id).NotEmpty().WithName($"User.{nameof(AuditLogUserDomainModel.Id)}");
        RuleFor(user => user.Ip).NotEmpty().WithName($"User.{nameof(AuditLogUserDomainModel.Ip)}");
        RuleFor(user => user.Login).NotEmpty().WithName($"User.{nameof(AuditLogUserDomainModel.Login)}");
        RuleFor(user => user.UserAgent).NotEmpty().WithName($"User.{nameof(AuditLogUserDomainModel.UserAgent)}");
    }
}