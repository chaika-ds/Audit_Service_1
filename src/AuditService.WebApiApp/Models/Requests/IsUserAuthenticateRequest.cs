namespace AuditService.WebApiApp.Models.Requests;

public class IsUserAuthenticateRequest
{
    /// <summary>
    /// Token
    /// </summary>
    public string Token { get; set; }
    
    /// <summary>
    /// Id ноды.
    /// </summary>
    public string NodeId { get; set; }
}