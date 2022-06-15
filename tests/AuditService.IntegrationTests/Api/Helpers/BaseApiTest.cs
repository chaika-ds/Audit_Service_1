using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Tolar.Authenticate;
using Tolar.Authenticate.Impl;


namespace AuditService.IntegrationTests.Api.Helpers;

public class BaseApiTest
{
    protected readonly IAuthenticateService Auth;
    protected readonly string Localhost;

    protected BaseApiTest()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        
        Localhost = configuration.GetSection("Project:Address").Value;
        
        Auth = new AuthenticateService(new TestSettings(), new HttpClient());
    }
    
}
