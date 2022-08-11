using AuditService.Common.Consts;
using Tolar.Authenticate;

namespace AuditService.SettingsService.ApiClient;

/// <summary>
///     Request handler to set headers before sending
/// </summary>
internal class AuthHeaderHandler : DelegatingHandler
{
    private readonly IAuthenticateService _authenticateService;

    public AuthHeaderHandler(IAuthenticateService authenticateService)
    {
        _authenticateService = authenticateService;
    }

    /// <summary>
    ///    Send request
    /// </summary>
    /// <param name="request">Request message</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response message</returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await _authenticateService.AuthenticationService();
        request.Headers.Add(HeaderNameConst.XNodeId, _authenticateService.NodeId.ToString());
        request.Headers.Add(HeaderNameConst.Token, _authenticateService.Token);
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}