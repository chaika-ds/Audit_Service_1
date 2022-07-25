namespace AuditService.Handlers.PipelineBehaviors.Attributes;

/// <summary>
///     Attribute for using pipeline behaviors
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class UsePipelineBehaviors : Attribute
{
    /// <summary>
    ///     Flag indicating that logging should be used
    /// </summary>
    public bool UseLogging { get; set; } = false;

    /// <summary>
    ///     Flag indicating that caching should be used
    /// </summary>
    public bool UseCache { get; set; } = false;

    /// <summary>
    ///     Cache lifetime in seconds
    /// </summary>
    public int CacheLifeTime { get; set; } = 600;
}