using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AuditService.Common.Models.Dto;

/// <summary>
///     Health Check Dto
/// </summary>
public class HealthCheckDto
{
    /// <summary>
    ///     Health Check Result
    /// </summary>
    public HealthCheckResult HealthCheckResult { get; set; }
    
    /// <summary>
    ///    Elapsed Milliseconds
    /// </summary>
    public long ElapsedMilliseconds { get; set; }
}