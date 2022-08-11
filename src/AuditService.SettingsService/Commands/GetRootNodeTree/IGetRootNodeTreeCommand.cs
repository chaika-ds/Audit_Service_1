using AuditService.SettingsService.ApiClient.Models;
using AuditService.SettingsService.Commands.BaseEntities;

namespace AuditService.SettingsService.Commands.GetRootNodeTree;

/// <summary>
///     Command to get a root node tree
/// </summary>
public interface IGetRootNodeTreeCommand : ISettingsServiceCommand
{
    /// <summary>
    ///     Execute сommand to getting root node tree
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Root node tree</returns>
    Task<NodeModel> ExecuteAsync(CancellationToken cancellationToken = default);
}