using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tolar.Authenticate;

namespace AuditService.Setup.Attributes;

/// <summary>
///     Authorization Attribute
/// </summary>
public class AuthorizationAttribute: AuthorizeAttribute
{
    /// <summary>
    ///    Constructor for Authorization Attribute
    /// </summary>
    public AuthorizationAttribute(string permission): base (permission) { }
    
    /// <summary>
    ///     Override Create Answer method from base
    /// </summary>
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