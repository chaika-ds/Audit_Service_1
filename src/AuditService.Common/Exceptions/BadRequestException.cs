namespace AuditService.Common.Exceptions;

/// <summary>
///     Throw new exception when we have incorrect request data
/// </summary>
public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }
}