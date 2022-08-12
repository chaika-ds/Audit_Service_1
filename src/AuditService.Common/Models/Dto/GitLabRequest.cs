using MediatR;

namespace AuditService.Common.Models.Dto
{
    /// <summary>
    /// GitLab  request
    /// </summary>
    public record GitLabRequest : IRequest<GitLabVersionResponseDto>;
}