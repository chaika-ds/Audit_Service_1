using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AuditService.Data.Domain.Enums;
using AuditService.IntegrationTests.Api.Helpers;


namespace AuditService.IntegrationTests.Api;
using Xunit;

public class ReferenceApiTest : BaseApiTest
{
    [Fact]
    public async Task GET_Referance_Services_Return_AllServiceAsync()
    {
        await Auth.AuthenticationService();
        
        var url = $"{Localhost}/reference/services";
        
        var response = await HttpHelper.SendAsync(url, "", Auth.NodeId.ToString(), Auth.Token, HttpMethod.Get);
        
        var resultRequest = await response.Content.ReadAsStringAsync();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(ServiceId.SSO.ToString(), resultRequest);
        Assert.Contains(ServiceId.PAYMENT.ToString(), resultRequest);
    }
    
    [Fact]
    public async Task GET_Referance_Categories_Return_AllCategoriesAsync()
    {
        await Auth.AuthenticationService();
        
        var url = $"{Localhost}/reference/categories";
        
        var response = await HttpHelper.SendAsync(url, "", Auth.NodeId.ToString(), Auth.Token, HttpMethod.Get);
        
        var resultRequest = await response.Content.ReadAsStringAsync();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(ServiceId.SSO.ToString(), resultRequest);
        Assert.Contains(ServiceId.PAYMENT.ToString(), resultRequest);
    }
    
    [Fact]
    public async Task GET_Referance_Categories_Return_AllCategoriesByServiceAsync()
    {
        await Auth.AuthenticationService();
        
        var url = $"{Localhost}/reference/categories/{ServiceId.PAYMENT.ToString()}";
        
        var response = await HttpHelper.SendAsync(url, "", Auth.NodeId.ToString(), Auth.Token, HttpMethod.Get);
        
        var resultRequest = await response.Content.ReadAsStringAsync();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(ServiceId.PAYMENT.ToString(), resultRequest);
    }
    
}