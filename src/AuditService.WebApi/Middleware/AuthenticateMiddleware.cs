using Tolar.Authenticate;

namespace AuditService.WebApi.Middleware
{
    /// <summary>
    ///     SSO Authentication
    /// </summary>
    public class AuthenticateMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthenticateService _authenticateService;


        public AuthenticateMiddleware(RequestDelegate next, IAuthenticateService authenticateService)
        {
            _next = next;
            _authenticateService = authenticateService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!(context.Request.Path.HasValue && context.Request.Path.Value.Equals("/_hc", StringComparison.InvariantCultureIgnoreCase)))
                await AuthenticateAsync(context);
            
            await _next(context);
        }

        /// <summary>
        ///     Autorize service and user in SSO
        /// </summary>
        /// <param name="context">Current context</param>
        private async Task AuthenticateAsync(HttpContext context)
        {
            await _authenticateService.AuthenticationService();

            var token = context.Request.Headers["Token"];
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token), "Token is null");

            var xNodeId = context.Request.Headers["X-Node-Id"];
            if (!Guid.TryParse(xNodeId, out var nodeId))
                throw new ArgumentNullException(nameof(xNodeId), "X-Node-Id is null");

            var user = await _authenticateService.GetIsUserAuthenticate(token, nodeId);
            if (user.Status != 200)
                throw new UnauthorizedAccessException("Failed to log in to the SSO");
        }
    }
}