using AuditService.SettingsService.ApiClient;
using AuditService.SettingsService.ApiClient.Models;
using AuditService.SettingsService.Storage;

namespace AuditService.SettingsService.Commands.GetRootNodeTree;

/// <summary>
///     Command to get a root node tree
///     Retrieving data from the storage, if necessary, accessing the source.
/// </summary>
internal class GetRootNodeTreeCommand : IGetRootNodeTreeCommand
{
    private readonly ISettingsServiceApiClient _apiClient;
    private readonly ISettingsServiceStorage _storage;

    public GetRootNodeTreeCommand(ISettingsServiceApiClient apiClient, ISettingsServiceStorage storage)
    {
        _apiClient = apiClient;
        _storage = storage;
    }

    /// <summary>
    ///     Execute сommand to getting root node tree
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Root node tree</returns>
    public async Task<NodeModel> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var nodeModel = await _storage.GetRootNodeTree(cancellationToken);

        if (nodeModel is not null)
            return nodeModel;

        var apiResult = await _apiClient.GetRootNodeTree();

        if (!apiResult.IsSuccessStatusCode || apiResult.Content is null)
            throw new InvalidOperationException("Getting root node failed");

        await _storage.SetRootNodeTree(apiResult.Content, cancellationToken);
        return apiResult.Content;
    }
}