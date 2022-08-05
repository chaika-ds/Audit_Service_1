using System.Net;
using FluentValidation;

namespace AuditService.Handlers.Validators;

/// <summary>
///     IP address validator
/// </summary>
public class IpAddressValidator : AbstractValidator<string>
{
    public IpAddressValidator()
    {
        RuleFor(ipAddress => ipAddress).Must(ipAddress => IPAddress.TryParse(ipAddress, out _)).WithMessage("IP address is invalid.");
    }
}