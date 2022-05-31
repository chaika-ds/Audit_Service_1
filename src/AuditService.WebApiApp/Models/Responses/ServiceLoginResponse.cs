namespace AuditService.WebApiApp.Models.Responses;

public abstract class ServiceLoginResponse
{
    /// <summary>
    /// Token
    /// </summary>
    public string Token { get; }
}