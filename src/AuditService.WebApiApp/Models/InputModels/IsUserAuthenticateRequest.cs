namespace AuditService.WebApiApp.Models.InputModels;

public class IsUserAuthenticateRequest
{
    public string Token { get; set; }
    public string NodeId { get; set; }
}