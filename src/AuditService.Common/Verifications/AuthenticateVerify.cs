using Microsoft.AspNetCore.Http;

namespace AuditService.Common.Verifications
{
    /// <summary>
    /// Checking endpoint for need authorization in SSO
    /// </summary>
    public class AuthenticateVerify
    {
        private static HashSet<string> endPoints = new HashSet<string>()
        {
            "/_hc"
        };

        /// <summary>
        /// Check endpoint
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool NeedAuth(HttpContext context)
        {
            return context.Request.Path.HasValue
                && endPoints.Any(x => x.Equals(context.Request.Path.Value, StringComparison.InvariantCultureIgnoreCase));            
        }
    }
}
