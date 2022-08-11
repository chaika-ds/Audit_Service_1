using Microsoft.Extensions.Configuration;

namespace AuditService.Setup.AppSettings;


/// <summary>
///     Gitlab settings implementation
/// </summary>
internal class GitlabSettings : IGitlabSettings
{
    public GitlabSettings(IConfiguration configuration) => ApplySettings(configuration);

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

    private void ApplySettings(IConfiguration configuration)
    {
        Url = configuration["Gitlab:Url"];
        ProjectId = configuration["Gitlab:ProjectId"];
        Username = configuration["Gitlab:Username"];
        Password = configuration["Gitlab:Password"];
        BranchName = configuration["Gitlab:BranchName"];
    }
}