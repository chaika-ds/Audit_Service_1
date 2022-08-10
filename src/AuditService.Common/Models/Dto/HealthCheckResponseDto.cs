namespace AuditService.Common.Models.Dto;

public class HealthCheckResponseDto
{
    public HealthCheckResponseDto()
    {
        Timestamp = DateTime.Now;
        Components = new Dictionary<string, ComponentsDto>();
        Version = new VersionDto();
    }

    public DateTime Timestamp { get; set;  }
    public IDictionary<string, ComponentsDto> Components { get; set; }
    public VersionDto Version { get; set; }
}