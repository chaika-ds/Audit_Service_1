using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AuditService.Data.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Tolar.Authenticate;
using Tolar.Authenticate.Impl;


namespace AuditService.IntegrationTests.Api;
using Xunit;

public class ReferanceApiTest
{
    private readonly IAuthenticateService _auth;
    private const string JsonMediaType = "application/json";
    private const string Localhost = "https://localhost:7181";
    
    public ReferanceApiTest()
    {
        _auth = new AuthenticateService(new TestSettings(), new HttpClient());
    }
    
    [Fact]
    public async Task GET_Referance_Services_Return_AllServiceAsync()
    {
        var url = $"{Localhost}/reference/services";
        
        var response = await Post(url);
        var resultRequest = await response.Content.ReadAsStringAsync();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(ServiceIdentity.SSOSERVICE.ToString(), resultRequest);
        Assert.Contains(ServiceIdentity.PAYMENTSERVICE.ToString(), resultRequest);
    }
    
    [Fact]
    public async Task GET_Referance_Categories_Return_AllCategoriesAsync()
    {
        var url = $"{Localhost}/reference/categories";
        
        var response = await Post(url);
        var resultRequest = await response.Content.ReadAsStringAsync();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(ServiceIdentity.SSOSERVICE.ToString(), resultRequest);
        Assert.Contains(ServiceIdentity.PAYMENTSERVICE.ToString(), resultRequest);
    }
    
    [Fact]
    public async Task GET_Referance_Categories_Return_AllCategoriesByServiceAsync()
    {
        var url = $"{Localhost}/reference/categories/{ServiceIdentity.PAYMENTSERVICE.ToString()}";
        
        var response = await Post(url);
        var resultRequest = await response.Content.ReadAsStringAsync();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(ServiceIdentity.PAYMENTSERVICE.ToString(), resultRequest);
    }

    private async Task<HttpResponseMessage> Post(string url)
    {
        await _auth.AuthenticationService();
        
        using var httpClient = new HttpClient();
        using var requestContent = new StringContent("", Encoding.UTF8, JsonMediaType);
        using var request = new HttpRequestMessage(HttpMethod.Get, url );
        
        request.Headers.Add("X-Node-Id", _auth.NodeId.ToString());
        request.Headers.Add("Token",  _auth.Token);
        request.Content = requestContent;
        
        return await httpClient.SendAsync(request);
    }
}