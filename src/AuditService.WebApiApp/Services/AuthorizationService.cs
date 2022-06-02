using System.Net;
using System.Net.Http.Headers;
using System.Text;
using AuditService.WebApiApp.Models.Requests;
using AuditService.WebApiApp.Models.Responses;
using AuditService.WebApiApp.Services.Interfaces;
using Nest;
using Newtonsoft.Json;

namespace AuditService.WebApiApp.Services;

public class AuthorizationService: IAuthorization
{
    private const string JsonMediaType = "application/json";
    
    private const string BaseUrl = "http://sso-api.netreportservice.xyz";
    private const string ServiceLogin = "account/servicelogin";
    private const string IsUserAuthenticate = "account/getisuserauthenticate";

    public async Task<ServiceLoginResponse> ServiceLoginAuthorization(ServiceLoginRequest svRequest)
    {
        const string relativeUri = $"{BaseUrl}/{ServiceLogin}";
        return await PostJson<ServiceLoginResponse>(relativeUri, svRequest);;
    }
    public async Task<IsUserAuthenticateResponse> GetIsUserAuthenticate(IsUserAuthenticateRequest inputModel)
    {
        string relativeUri = $"{BaseUrl}/{IsUserAuthenticate}?token={inputModel.Token}&nodeId={inputModel.NodeId}";
        
        using var httpClient = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, relativeUri);
        
        request.Headers.Add("Token", inputModel.Token);
        request.Headers.Add("X-Node-Id", inputModel.NodeId);

        using var response = await httpClient.SendAsync(request);
        
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException();
        }

        var contentString= await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<IsUserAuthenticateResponse>(contentString);
    }
    
    private static async Task<TResponse> PostJson<TResponse>(string relativeUri, object requestBody, bool ensureSuccessStatusCode = true)
    {
        var json = JsonConvert.SerializeObject(requestBody);

        using var requestContent = new StringContent(json, Encoding.UTF8, JsonMediaType);
        using var request = new HttpRequestMessage(HttpMethod.Post, relativeUri);

        request.Content = requestContent;
        
        using var httpClient = new HttpClient();
        using var response = await httpClient.SendAsync(request).ConfigureAwait(false);

        if (ensureSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }

            response.EnsureSuccessStatusCode();
        }

        var contentString= await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<TResponse>(contentString);
    }
}