namespace AuditService.Common.Models.Domain;

public class EventDomainModel
{
    public string Event { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string[] ChangeableAttributes { get; set; }
    public string OldOrNewValue { get; set; }
}