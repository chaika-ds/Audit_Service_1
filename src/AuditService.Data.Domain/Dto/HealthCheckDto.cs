namespace AuditService.Data.Domain.Dto;

/// <summary>
///  Checking Health of Services.
/// </summary>
public class HealthCheckDto
{
    /// <summary>
    /// Is Kafka healthy.
    /// </summary>
    public bool Kafka { get; set; }
    /// <summary>
    /// Is Elk healthy
    /// </summary>
    public bool Elk { get; set; }
}