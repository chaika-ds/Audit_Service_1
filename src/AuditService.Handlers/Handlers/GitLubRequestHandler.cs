using AuditService.Common.Models.Dto;
using AuditService.Setup.AppSettings;
using GitLabApiClient;
using MediatR;

namespace AuditService.Handlers.Handlers;

/// <summary>
///     GitLub request handler
/// </summary>
public class GitLubRequestHandler : IRequestHandler<GitLubRequest, GitLubVersionResponseDto>
{
    private readonly IGitlabSettings _gitlabSettings;
    private readonly IGitLabClient _gitLabClient;

    public GitLubRequestHandler(IGitLabClient gitLabClient, IGitlabSettings gitlabSettings)
    {
        _gitLabClient = gitLabClient;
        _gitlabSettings = gitlabSettings;
    }

    /// <summary>
    ///     Handle a request for a the GitLub service
    /// </summary>
    /// <param name="request">GitLub request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>GitLub version response dto</returns>
    public async Task<GitLubVersionResponseDto> Handle(GitLubRequest request, CancellationToken cancellationToken)
    {
        await _gitLabClient.LoginAsync(_gitlabSettings.Username, _gitlabSettings.Password);

        var branchInfo = await _gitLabClient.Branches.GetAsync(_gitlabSettings.ProjectId, _gitlabSettings.BranchName);

        var tags = await _gitLabClient.Tags.GetAsync(_gitlabSettings.ProjectId);

        return new GitLubVersionResponseDto
        {
            Branch = branchInfo.Name,
            Commit = branchInfo.Commit.Id,
            Tag = tags.MaxBy(x => x.Commit.CreatedAt)?.Name
        };
    }
}