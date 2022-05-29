namespace AuditService.WebApiApp.Models.InputModels;

public class ServiceLoginRequest
{
    public Guid ServiceId { get; set; }
    public string ApiKey { get; set; }
}