using AuditService.Common.Extensions;
using AuditService.Common.Models.Dto.Filter;
using FluentValidation;

namespace AuditService.Handlers.Validators;

/// <summary>
///     ILogFilter validator
/// </summary>
public class LogFilterValidator : AbstractValidator<ILogFilter>
{
    public LogFilterValidator()
    {
        RuleFor(model => model.TimestampFrom).NotEmpty().Must(timestamp=> timestamp != default).WithMessage("The start date is required.");
        RuleFor(model => model.TimestampTo).NotEmpty().Must(timestamp=> timestamp != default).WithMessage("The end date is required.");
        RuleFor(model => model.TimestampFrom)
            .Must((model, timestamp) => timestamp < model.TimestampTo && timestamp.DetermineMonthDifference(model.TimestampTo) <= 3).WithMessage("Invalid filter range time.");
    }
}