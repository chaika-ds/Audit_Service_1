namespace AuditService.Common.Models.Dto;

public class VersionDto
{
    public string Branch { get; set; }
    public string Commit { get; set; }
    public string Tag { get; set; }
}