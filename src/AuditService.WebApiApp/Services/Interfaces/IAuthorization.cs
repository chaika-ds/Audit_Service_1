using AuditService.WebApiApp.Models.Requests;
using AuditService.WebApiApp.Models.Responses;

namespace AuditService.WebApiApp.Services.Interfaces;

public interface IAuthorization
{
    Task<ServiceLoginResponse> ServiceLoginAuthorization(ServiceLoginRequest svRequest);
    Task<IsUserAuthenticateResponse> GetIsUserAuthenticate(IsUserAuthenticateRequest isUserAuthenticateRequest);
}