using AuditService.Tests.Fakes.ServiceData;
using KIT.Minio.HealthCheck;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.Tests.Minio;

/// <summary>
///     HealthCheck test
/// </summary>
public class HealthCheckTest
{
    /// <summary>
    ///     Check if the HealthCheck returned false status
    /// </summary>
    [Fact]
    public async Task HealthCheck_CreateBrockenMinioEnviroment_FalseStatusOfHealthCheck()
    {
        //Arrange
        var serviceProvider = ServiceProviderFake.GetServiceProviderForMinio();

        var healthCheck = serviceProvider.GetRequiredService<IMinioHealthCheck>();

        //Act 
        var actualHealthCheckResult = await healthCheck.CheckHealthAsync();

        //Assert
        False(actualHealthCheckResult.Status);
    }
}