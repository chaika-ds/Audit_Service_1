namespace AuditService.Common.Models.Domain;

/// <summary>
///     Events data model
/// </summary>
public class EventDomainModel
{
    public EventDomainModel()
    {
        Event = string.Empty;
        Name = string.Empty;
        Description = string.Empty;
        ChangeableAttributes = Array.Empty<string>();
        OldOrNewValue = string.Empty;
    }

    /// <summary>
    ///     Type of event
    /// </summary>
    public string Event { get; set; }
    
    /// <summary>
    ///     Name of event
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    ///     Description of event
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    ///     Changeable Attributes of event
    /// </summary>
    public string[] ChangeableAttributes { get; set; }
    
    /// <summary>
    ///     Old Or New Value of event
    /// </summary>
    public string OldOrNewValue { get; set; }
}