using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tolar.Authenticate;

namespace AuditService.Setup.Attributes;

public class CustomAuthorizeAttribute: AuthorizeAttribute
{
    
    public CustomAuthorizeAttribute(string permission): base (permission) { }
    
    protected override ObjectResult CreateAnswer(HttpStatusCode code, string specialMessage = null)
    {
        IHttpContextAccessor context = new HttpContextAccessor();
        

        var resultObject = new ProblemDetails
        {
            Status = (int)code,
            Title = specialMessage,
            Instance = context?.HttpContext?.Request.Path,
            Type = code.ToString()
        };

        return new ObjectResult(resultObject);
    }
}