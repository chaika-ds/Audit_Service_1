using AuditService.Common.Models.Domain;
using FluentValidation;

namespace KIT.Kafka.Consumers.VisitLog.Validators;

/// <summary>
///     User role model validator
/// </summary>
public class UserRoleDomainModelValidator : AbstractValidator<UserRoleDomainModel>
{
    public UserRoleDomainModelValidator()
    {
        RuleFor(userRoleDomainModel => userRoleDomainModel.Name).NotEmpty().WithName($"UserRole.{nameof(UserRoleDomainModel.Name)}");
        RuleFor(userRoleDomainModel => userRoleDomainModel.Code).NotEmpty().WithName($"UserRole.{nameof(UserRoleDomainModel.Code)}");
    }
}