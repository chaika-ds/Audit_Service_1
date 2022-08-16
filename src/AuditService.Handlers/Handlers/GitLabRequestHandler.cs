using AuditService.Common.Models.Dto;
using AuditService.Setup.AppSettings;
using GitLabApiClient;
using KIT.NLog.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuditService.Handlers.Handlers;

/// <summary>
///     GitLab request handler
/// </summary>
public class GitLabRequestHandler : IRequestHandler<GitLabRequest, GitLabVersionResponseDto>
{
    private readonly IGitlabSettings _gitlabSettings;
    private readonly IGitLabClient _gitLabClient;
    private readonly ILogger<GitLabRequestHandler> _logger;
    public GitLabRequestHandler(IGitLabClient gitLabClient, IGitlabSettings gitlabSettings, ILogger<GitLabRequestHandler> logger)
    {
        _gitLabClient = gitLabClient;
        _gitlabSettings = gitlabSettings;
        _logger = logger;
    }

    /// <summary>
    ///     Handle a request for a the GitLub service
    /// </summary>
    /// <param name="request">GitLub request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>GitLab version response dto</returns>
    public async Task<GitLabVersionResponseDto> Handle(GitLabRequest request, CancellationToken cancellationToken)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogException(ex, $"Check GitLab healthy on {DateTime.UtcNow}");
            return new GitLabVersionResponseDto();
        }
    }
}