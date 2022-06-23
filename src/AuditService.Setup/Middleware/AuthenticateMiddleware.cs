using AuditService.Common.Enums;
using AuditService.Setup.Extensions;
using Microsoft.AspNetCore.Http;
using Nest;
using Newtonsoft.Json;
using Tolar.Authenticate;
using Tolar.Authenticate.Dtos;
using Tolar.Redis;

namespace AuditService.Setup.Middleware
{
    /// <summary>
    ///     SSO Authentication
    /// </summary>
    public class AuthenticateMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthenticateService _authenticateService;
        private readonly IRedisRepository _redis;

        private string _token = string.Empty;
        private Guid _nodeId;


        public AuthenticateMiddleware(RequestDelegate next, IAuthenticateService authenticateService,
            IRedisRepository redis)
        {
            _next = next;
            _authenticateService = authenticateService;
            _redis = redis;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!(context.Request.Path.HasValue &&
                  context.Request.Path.Value.Equals("/_hc", StringComparison.InvariantCultureIgnoreCase)))
                await AuthenticateAsync(context);

            await _next(context);
        }

        /// <summary>
        ///     Autorize service and user in SSO
        /// </summary>
        /// <param name="context">Current context</param>
        private async Task AuthenticateAsync(HttpContext context)
        {
            var isTokenFromCache = true;

            var cachedTokenKey = (nameof(AuthenticateMiddleware) + _authenticateService.NodeId).GetHash(HashType.MD5);
            var cachedToken = await _redis.GetAsync<string>(cachedTokenKey);

            if (cachedToken == null)
            {
                await AuthenticationServiceAsync(context);
                isTokenFromCache = false;
            }

            var cachedUserKey = (nameof(AuthenticateMiddleware) + _nodeId + _token).GetHash(HashType.MD5);
            var cachedUser = await _redis.GetAsync<AuthenticatedResponse>(cachedUserKey);

            if (cachedUser == null)
            {
                var user = await GetIsUserAuthenticateAsync(context, isTokenFromCache, cachedTokenKey);

                await _redis.SetAsync(cachedUserKey, JsonConvert.SerializeObject(user), TimeSpan.FromMinutes(user.TokenLifeTimeInMinutes));

                await _redis.SetAsync(cachedTokenKey, JsonConvert.SerializeObject(_token), TimeSpan.FromMinutes(user.TokenLifeTimeInMinutes));
            }
        }

        private async Task AuthenticationServiceAsync(HttpContext context)
        {
            await _authenticateService.AuthenticationService();

            _token = context.Request.Headers["Token"].ToString();
            if (string.IsNullOrEmpty(_token))
                throw new ArgumentNullException(nameof(_token), "Token is null");

            var xNodeId = context.Request.Headers["X-Node-Id"].ToString();
            if (!Guid.TryParse(xNodeId, out _nodeId))
                throw new ArgumentNullException(nameof(xNodeId), "X-Node-Id is null");
        }

        private async Task<AuthenticatedResponse> GetIsUserAuthenticateAsync(HttpContext context, bool isTokenFromCache, string cachedTokenKey)
        {
            var user = await _authenticateService.GetIsUserAuthenticate(_token, _nodeId);

            if (user.Status == 200)
                return user;

            if (!isTokenFromCache)
                throw new UnauthorizedAccessException("Failed to log in to the SSO");

            await _redis.DeleteAsync(cachedTokenKey);

            await AuthenticationServiceAsync(context);

            user = await GetIsUserAuthenticateAsync(context, isTokenFromCache, cachedTokenKey);

            return user;
        }
    }
}