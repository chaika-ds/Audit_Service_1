using AuditService.Common.Enums;
using AuditService.SettingsService.ApiClient.Models;

namespace AuditService.SettingsService.Extensions;

/// <summary>
///     Extension for working with the node model
/// </summary>
public static class NodeModelExtension
{
    /// <summary>
    ///     Include all children node
    /// </summary>
    /// <param name="rootNode">Node model(settings service model)</param>
    /// <returns>All node children</returns>
    public static IEnumerable<NodeModel> IncludeChildren(this NodeModel rootNode) => GetAllDependencies(rootNode);

    /// <summary>
    ///     Find node
    /// </summary>
    /// <param name="rootNode">Root node</param>
    /// <param name="uuid">Node uuid</param>
    /// <returns>Found node</returns>
    public static NodeModel? FindNode(this NodeModel rootNode, Guid uuid) => 
        rootNode.Uuid == uuid ? rootNode : rootNode.IncludeChildren().FirstOrDefault(child => child.Uuid == uuid);

    /// <summary>
    ///     Select node Ids for current node
    /// </summary>
    /// <param name="rootNode">Root node</param>
    /// <param name="uuid">Current node uuid</param>
    /// <returns>Selected Ids</returns>
    public static IEnumerable<Guid> SelectNodeIdsForCurrent(this NodeModel rootNode, Guid uuid)
    {
        var currentNode = rootNode.FindNode(uuid);

        if (currentNode is null)
            yield break;

        if (currentNode.Type == NodeType.HALL)
            yield return currentNode.Uuid;

        foreach (var childNode in currentNode.IncludeChildren())
            yield return childNode.Uuid;
    }

    /// <summary>
    ///     Get all dependencies
    /// </summary>
    /// <param name="nodeModel">Node model(settings service model)</param>
    /// <returns>All node dependencies</returns>
    private static IEnumerable<NodeModel> GetAllDependencies(NodeModel nodeModel)
    {
        yield return nodeModel;

        if (nodeModel.Children is null || !nodeModel.Children.Any())
            yield break;

        foreach (var childNode in nodeModel.Children.SelectMany(GetAllDependencies))
            yield return childNode;
    }
}