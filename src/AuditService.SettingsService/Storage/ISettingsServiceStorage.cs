using AuditService.SettingsService.ApiClient.Models;

namespace AuditService.SettingsService.Storage;

/// <summary>
///     Storage for settings service resources
/// </summary>
public interface ISettingsServiceStorage
{
    /// <summary>
    ///     Get root node tree
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Root node tree</returns>
    Task<NodeModel?> GetRootNodeTree(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Set root node tree
    /// </summary>
    /// <param name="model">Root node tree</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task execution result</returns>
    Task SetRootNodeTree(NodeModel model, CancellationToken cancellationToken = default);
}