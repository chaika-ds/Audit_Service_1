namespace AuditService.Setup.AppSettings;

/// <summary>
///     Contract for Gitlab settings
/// </summary>
public interface IGitlabSettings
{
    /// <summary>
    ///     Repository URL
    /// </summary>
    public string Url { get; set; }
    
    /// <summary>
    ///     Project Id 
    /// </summary>
    public string ProjectId { get; set; }
    
    /// <summary>
    ///     Credential username
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    ///     Credentials password
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    ///     Branch name
    /// </summary>
    public string BranchName { get; set; }
}