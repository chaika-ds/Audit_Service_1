using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using FluentValidation;

namespace KIT.Kafka.Consumers.VisitLog.Validators;

/// <summary>
///     Visit log consumer message model validator
/// </summary>
public class VisitLogConsumerMessageValidator : AbstractValidator<VisitLogConsumerMessage>
{
    public VisitLogConsumerMessageValidator(IValidator<UserRoleDomainModel> userRoleValidator)
    {
        RuleFor(message => message.ProjectId).NotEmpty();
        RuleFor(message => message.Login).NotEmpty();
        RuleFor(message => message.Ip).NotEmpty();
        RuleFor(message => message.Timestamp).NotEmpty();
        RuleFor(message => message.Authorization).NotNull();

        When(message => message.Type == VisitLogType.Player, () =>
        {
            RuleFor(model => model.HallId).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(model => model.PlayerId).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(model => model.Authorization)
                .SetValidator(new AuthorizationDataDomainModelValidator(VisitLogType.Player));
        }).Otherwise(() =>
        {
            RuleFor(model => model.NodeId).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(model => model.NodeType).NotNull();
            RuleFor(model => model.UserId).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(model => model.UserRoles).NotEmpty();
            RuleForEach(model => model.UserRoles).SetValidator(userRoleValidator);
            RuleFor(model => model.Authorization)
                .SetValidator(new AuthorizationDataDomainModelValidator(VisitLogType.User));
        });
    }
}