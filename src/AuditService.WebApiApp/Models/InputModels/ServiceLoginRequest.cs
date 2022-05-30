namespace AuditService.WebApiApp.Models.InputModels;

public class ServiceLoginRequest
{
    /// <summary>
    /// ServiceId
    /// </summary>
    public Guid ServiceId { get; set; }
    
    /// <summary>
    /// ApiKey
    /// </summary>
    public string ApiKey { get; set; }
}