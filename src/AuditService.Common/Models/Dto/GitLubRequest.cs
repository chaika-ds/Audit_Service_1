using MediatR;

namespace AuditService.Common.Models.Dto
{
    /// <summary>
    /// GitLub  request
    /// </summary>
    public record GitLubRequest : IRequest<GitLubVersionResponseDto>;
}