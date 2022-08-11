namespace AuditService.Setup.AppSettings;

/// <summary>
///     Contract for Gitlab settings
/// </summary>
public interface IGitlabSettings
{
    public string Url { get; set; }
    public string ProjectId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string BranchName { get; set; }
}