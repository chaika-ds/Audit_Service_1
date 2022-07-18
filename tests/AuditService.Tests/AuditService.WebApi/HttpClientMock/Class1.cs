using System.Reflection;

using Tolar.Authenticate;

namespace AuditService.Tests.AuditService.WebApi.HttpClientMock
{
    internal class Class1
    {
        internal static HttpClient GetAuthenticateServiceField(IAuthenticateService myHttpClient)
        {
            var cc = myHttpClient
                         .GetType()
                         .GetFields(BindingFlags.Instance | BindingFlags.GetField | BindingFlags.NonPublic)
                         .FirstOrDefault(x => x.FieldType == typeof(HttpClient))
                         ?.GetValue(myHttpClient) as HttpClient
                     ?? throw new Exception("Failed to find a HttpClient field!");
            return cc;
        }
    }
}
