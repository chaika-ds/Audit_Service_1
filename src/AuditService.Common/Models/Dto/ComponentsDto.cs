namespace AuditService.Common.Models.Dto;

/// <summary>
///    Component Model
/// </summary>
public class ComponentsDto
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