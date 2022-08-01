using FluentValidation;

namespace KIT.Kafka.Consumers.BlockedPlayersLog.Validators;

/// <summary>
///     Blocked players log consumer message model validator
/// </summary>
public class BlockedPlayersLogConsumerMessageValidator : AbstractValidator<BlockedPlayersLogConsumerMessage>
{
    public BlockedPlayersLogConsumerMessageValidator()
    {
        RuleFor(message => message.HallName).NotEmpty();
        RuleFor(message => message.ProjectId).NotEmpty();
        RuleFor(message => message.ProjectName).NotEmpty();
        RuleFor(message => message.LastVisitIpAddress).NotEmpty();
        RuleFor(message => message.Platform).NotEmpty();
        RuleFor(message => message.HallId).NotEmpty();
        RuleFor(message => message.PlayerLogin).NotEmpty();
        RuleFor(message => message.PlayerId).NotEmpty();
        RuleFor(message => message.BlockingDate).NotEmpty();
        RuleFor(message => message.Browser).NotEmpty();
        RuleFor(message => message.BrowserVersion).NotEmpty();
        RuleFor(message => message.Language).NotEmpty();
    }
}