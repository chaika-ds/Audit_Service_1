namespace AuditService.Common.ELK;

public class ElasticOptions
{
    public Uri[] Uris { get; set; }
    public string DefaultIndex { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}