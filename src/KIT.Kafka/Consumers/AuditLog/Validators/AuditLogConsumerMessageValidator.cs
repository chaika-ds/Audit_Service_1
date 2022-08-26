using AuditService.Common.Models.Domain.AuditLog;
using FluentValidation;

namespace KIT.Kafka.Consumers.AuditLog.Validators;

/// <summary>
///     Audit log consumer message model validator
/// </summary>
public class AuditLogConsumerMessageValidator : AbstractValidator<AuditLogConsumerMessage>
{
    public AuditLogConsumerMessageValidator(IValidator<AuditLogUserDomainModel> userValidator)
    {
        RuleFor(message => message.NodeId).NotEmpty();
        RuleFor(message => message.CategoryCode).NotEmpty();
        RuleFor(message => message.Timestamp).NotEmpty();
        RuleFor(message => message.User).NotNull();
        RuleFor(message => message.User).SetValidator(userValidator);
    }
}