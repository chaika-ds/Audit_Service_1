using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.VisitLog;
using FluentValidation;

namespace KIT.Kafka.Consumers.VisitLog.Validators;

/// <summary>
///     Authorization data model validator
/// </summary>
public class AuthorizationDataDomainModelValidator : AbstractValidator<AuthorizationDataDomainModel>
{
    public AuthorizationDataDomainModelValidator(VisitLogType type)
    {
        var modelName = nameof(BaseVisitLogDomainModel.Authorization);

        RuleFor(authorizationData => authorizationData.DeviceType).NotEmpty()
            .WithName($"{modelName}.{nameof(AuthorizationDataDomainModel.DeviceType)}");

        RuleFor(authorizationData => authorizationData.OperatingSystem).NotEmpty()
            .WithName($"{modelName}.{nameof(AuthorizationDataDomainModel.OperatingSystem)}");

        RuleFor(authorizationData => authorizationData.Browser).NotEmpty()
            .WithName($"{modelName}.{nameof(AuthorizationDataDomainModel.Browser)}");

        When(_ => type == VisitLogType.Player, () =>
        {
            RuleFor(authorizationData => authorizationData.AuthorizationType).NotEmpty()
                .WithName($"{modelName}.{nameof(AuthorizationDataDomainModel.AuthorizationType)}");
        });
    }
}