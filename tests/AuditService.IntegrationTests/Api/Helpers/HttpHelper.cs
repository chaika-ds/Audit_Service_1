using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace AuditService.IntegrationTests.Api.Helpers;

public static class HttpHelper
{
    private const string JsonMediaType = "application/json";

    public static async Task<HttpResponseMessage> SendAsync(string url, object body, string nodeId, string token, HttpMethod method)
    {
        using var httpClient = new HttpClient();
        
        var json = JsonConvert.SerializeObject(body);

        using var requestContent = new StringContent(json, Encoding.UTF8, JsonMediaType);
        using var request = new HttpRequestMessage(method, url);

        request.Headers.Add("X-Node-Id", nodeId);
        request.Headers.Add("Token",  token);
        request.Content = requestContent;

        return await httpClient.SendAsync(request);
       
    }
}
