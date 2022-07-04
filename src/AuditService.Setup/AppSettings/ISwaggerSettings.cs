namespace AuditService.Setup.AppSettings;

/// <summary>
///     Configuration section of swagger
/// </summary>
public interface ISwaggerSettings
{
    /// <summary>
    ///     XML comments for swagger
    /// </summary>
    public string[] XmlComments { get; set; }
}