namespace AuditService.Common.Models.Domain.AuditLog;

/// <summary>
///     Audit log attribute model
/// </summary>
public class AuditLogAttributeDomainModel
{
    public AuditLogAttributeDomainModel()
    {
        Key = string.Empty;
        Value = string.Empty;
    }

    /// <summary>
    ///     Attribute key
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    ///     Attribute value
    /// </summary>
    public string Value { get; set; }
}