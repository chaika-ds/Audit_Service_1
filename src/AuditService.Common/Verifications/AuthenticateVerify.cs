using Microsoft.AspNetCore.Http;

namespace AuditService.Common.Verifications
{
    public class AuthenticateVerify
    {
        private static HashSet<string> endPoints = new HashSet<string>()
        {
            "/_hc"
        };

        public static bool NeedAuith(HttpContext context)
        {
            return context.Request.Path.HasValue
                && endPoints.Any(x => x.Equals(context.Request.Path.Value, StringComparison.InvariantCultureIgnoreCase));            
        }
    }
}
