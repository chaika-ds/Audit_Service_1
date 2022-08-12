namespace AuditService.Common.Models.Dto;

/// <summary>
///    Health Check Component Model
/// </summary>
public class HealthCheckComponentsDto
{
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