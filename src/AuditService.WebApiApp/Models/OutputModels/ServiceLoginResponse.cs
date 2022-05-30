namespace AuditService.WebApiApp.Models.OutputModels;

public abstract class ServiceLoginResponse
{
    /// <summary>
    /// Token
    /// </summary>
    public string Token { get; }
}