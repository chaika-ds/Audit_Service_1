using System.Net;

namespace AuditService.Data.Domain.Dto;

/// <summary>
///     Default error response
/// </summary>
public class ErrorResponseDto
{
    /// <summary>
    ///     Http code status
    /// </summary>
    public HttpStatusCode Code { get; set; }

    /// <summary>
    ///     Message
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    ///     Full details of exception
    /// </summary>
    public string? StackTrace { get; set; }
}