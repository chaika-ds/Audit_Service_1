using AuditService.Common.Enums;
using AuditService.SettingsService.ApiClient.Models;
using AuditService.SettingsService.Commands.GetRootNodeTree;

namespace AuditService.Tests.Fakes.SettingsService;

/// <summary>
///     Fake command to get a root node tree
/// </summary>
internal class GetRootNodeTreeCommandFake : IGetRootNodeTreeCommand
{
    /// <summary>
    ///     Execute сommand to getting root node tree
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Root node tree</returns>
    public Task<NodeModel> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new NodeModel
        {
            Type = NodeType.HALL,
            Uuid = Guid.NewGuid(),
            Id = 0,
            Title = "Test node"
        });
    }
}