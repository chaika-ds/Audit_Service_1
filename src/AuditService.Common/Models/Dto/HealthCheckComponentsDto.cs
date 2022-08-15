namespace AuditService.Common.Models.Dto;

/// <summary>
///    Health Check Component Model
/// </summary>
public class HealthCheckComponentsDto
{
    public HealthCheckComponentsDto()
    {
        Name = string.Empty;
    }

    /// <summary>
    ///    RequestTime in milliseconds
    /// </summary>
    public decimal RequestTime { get; set; }
    
    /// <summary>
    ///    Name of service
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    ///    Status of service
    /// </summary>
    public bool Status { get; set; }
}