using AuditService.WebApiApp.Models.InputModels;
using AuditService.WebApiApp.Models.OutputModels;

namespace AuditService.WebApiApp.Services.Interfaces;

public interface IAuthorization
{
    Task<ServiceLoginResponse> ServiceLoginAuthorization(ServiceLoginRequest svRequest);
    Task<IsUserAuthenticateResponse> GetIsUserAuthenticate(IsUserAuthenticateRequest isUserAuthenticateRequest);
}