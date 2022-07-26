using AuditService.Common.Models.Domain;
using FluentValidation;

namespace KIT.Kafka.Consumers.AuditLog;

/// <summary>
///     Audit log consumer message model validator
/// </summary>
public class AuditLogConsumerMessageValidator : AbstractValidator<AuditLogConsumerMessage>
{
    public AuditLogConsumerMessageValidator(IValidator<IdentityUserDomainModel> userValidator)
    {
        RuleFor(message => message.NodeName).NotEmpty();
        RuleFor(message => message.NodeId).NotEmpty();
        RuleFor(message => message.CategoryCode).NotEmpty();
        RuleFor(message => message.Timestamp).NotEmpty();
        RuleFor(message => message.EntityName).NotEmpty();
        RuleFor(message => message.EntityId).NotEmpty();
        RuleFor(message => message.ProjectId).NotEmpty();
        RuleFor(message => message.User).NotNull();
        RuleFor(message => message.User).SetValidator(userValidator);
    }
}