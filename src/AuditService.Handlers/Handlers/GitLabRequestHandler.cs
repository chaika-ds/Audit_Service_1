using AuditService.Common.Models.Dto;
using AuditService.Setup.AppSettings;
using GitLabApiClient;
using MediatR;

namespace AuditService.Handlers.Handlers;

/// <summary>
///     GitLab request handler
/// </summary>
public class GitLabRequestHandler : IRequestHandler<GitLabRequest, GitLabVersionResponseDto>
{
    private readonly IGitlabSettings _gitlabSettings;
    private readonly IGitLabClient _gitLabClient;

    public GitLabRequestHandler(IGitLabClient gitLabClient, IGitlabSettings gitlabSettings)
    {
        _gitLabClient = gitLabClient;
        _gitlabSettings = gitlabSettings;
    }

    /// <summary>
    ///     Handle a request for a the GitLub service
    /// </summary>
    /// <param name="request">GitLub request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>GitLab version response dto</returns>
    public async Task<GitLabVersionResponseDto> Handle(GitLabRequest request, CancellationToken cancellationToken)
    {
        await _gitLabClient.LoginAsync(_gitlabSettings.Username, _gitlabSettings.Password);

        var branchInfo = await _gitLabClient.Branches.GetAsync(_gitlabSettings.ProjectId, _gitlabSettings.BranchName);

        var tags = await _gitLabClient.Tags.GetAsync(_gitlabSettings.ProjectId);

        return new GitLabVersionResponseDto
        {
            Branch = branchInfo.Name,
            Commit = branchInfo.Commit.Id,
            Tag = tags.MaxBy(x => x.Commit.CreatedAt)?.Name
        };
    }
}