using System.Net;

namespace AuditService.Common.Logger
{
    public class ErrorResponseDto
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }
    }
}
