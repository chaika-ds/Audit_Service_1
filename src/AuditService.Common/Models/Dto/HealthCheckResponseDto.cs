namespace AuditService.Common.Models.Dto;

/// <summary>
///     Health check response dto
/// </summary>
public class HealthCheckResponseDto
{
    public HealthCheckResponseDto()
    {
        Timestamp = DateTime.Now;
        Components = new Dictionary<string, HealthCheckComponentsDto>();
        HealthCheckVersion = new HealthCheckVersionDto();
    }

    /// <summary>
    ///     Timestamp
    /// </summary>
    public DateTime Timestamp { get; set;  }
    
    /// <summary>
    ///     Components
    /// </summary>
    public IDictionary<string, HealthCheckComponentsDto> Components { get; set; }
    
    /// <summary>
    ///     Version
    /// </summary>
    public HealthCheckVersionDto HealthCheckVersion { get; set; }
}