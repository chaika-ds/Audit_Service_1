namespace AuditService.Common.Contexts;

/// <summary>
///     Request context
/// </summary>
public class RequestContext
{
    /// <summary>
    ///     X-Node-Id
    /// </summary>
    public string? XNodeId { get; set; }

    /// <summary>
    ///     Language for localization
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    ///     Get a node or throw an exception
    /// </summary>
    /// <returns>X-Node-Id</returns>
    /// <exception cref="InvalidOperationException">Exception if node is missing</exception>
    public Guid GetRequiredXNodeId() => !string.IsNullOrEmpty(XNodeId) && Guid.TryParse(XNodeId, out var xnodeId) ? xnodeId
            : throw new InvalidOperationException("The request is missing a X-Node-Id");
}