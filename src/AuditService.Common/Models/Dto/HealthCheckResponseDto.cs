namespace AuditService.Common.Models.Dto;

/// <summary>
///     Health check response dto
/// </summary>
public class HealthCheckResponseDto
{
    public HealthCheckResponseDto()
    {
        Timestamp = DateTime.Now;
        Components = new Dictionary<string, ComponentsDto>();
        Version = new VersionDto();
    }

    /// <summary>
    ///     Timestamp
    /// </summary>
    public DateTime Timestamp { get; set;  }
    
    /// <summary>
    ///     Components
    /// </summary>
    public IDictionary<string, ComponentsDto> Components { get; set; }
    
    /// <summary>
    ///     Version
    /// </summary>
    public VersionDto Version { get; set; }
}