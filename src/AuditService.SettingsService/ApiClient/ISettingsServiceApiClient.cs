using AuditService.SettingsService.ApiClient.Models;
using Refit;

namespace AuditService.SettingsService.ApiClient;

/// <summary>
///     API client for interacting with the settings service
/// </summary>
public interface ISettingsServiceApiClient
{
    /// <summary>
    ///     Get the root node tree
    /// </summary>
    /// <returns>API response</returns>
    [Get("/trees/getTreeStructure")]
    Task<ApiResponse<NodeModel>> GetRootNodeTree();
}