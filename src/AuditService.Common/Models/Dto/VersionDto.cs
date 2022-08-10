namespace AuditService.Common.Models.Dto;

/// <summary>
///    Version of project 
/// </summary>
public class VersionDto
{
    /// <summary>
    ///    Current branch name
    /// </summary>
    public string? Branch { get; set; }
    
    /// <summary>
    ///   Last commit of current branch
    /// </summary>
    public string? Commit { get; set; }
    
    /// <summary>
    ///  Assigned tag to current branch 
    /// </summary>
    public string? Tag { get; set; }
}