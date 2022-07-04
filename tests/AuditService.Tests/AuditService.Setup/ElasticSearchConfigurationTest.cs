using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AuditService.Tests.AuditService.Setup;

public class ElasticSearchConfigurationTest
{
    [Fact]
    public void CollectionServicesTest()
    {
        var servicesMock = new Mock<ServiceCollection>();
        var configuration = new Mock<IConfiguration>();
        configuration.SetupGet(x => x[It.IsAny<string>()]).Returns("0");

        //var services = servicesMock.ge;

        //services.RegisterSettings();

        //services.AddElasticSearch();
        //services.AddRedisCache();
        //services.RegisterServices();

        //var serviceProvider = services.BuildServiceProvider();

        //var redisSettings = serviceProvider.GetRequiredService<IRedisSettings>();
        //Assert.NotNull(redisSettings);

        //var redis = serviceProvider.GetRequiredService<IDistributedCache>();
        //Assert.NotNull(redis);

        //var elk = serviceProvider.GetRequiredService<IElasticClient>();
        //Assert.NotNull(elk);

        //var authenticate = serviceProvider.GetRequiredService<IAuthenticateService>();
        //Assert.NotNull(authenticate);

        //var kafkaProducer = serviceProvider.GetRequiredService<IKafkaProducer>();
        //Assert.NotNull(kafkaProducer);

        //var healthService = serviceProvider.GetRequiredService<IHealthService>();
        //Assert.NotNull(healthService);

        //var referenceProvider = serviceProvider.GetRequiredService<IReferenceProvider>();
        //Assert.NotNull(referenceProvider);

        //var auditLogProvider = serviceProvider.GetRequiredService<IAuditLogProvider>();
        //Assert.NotNull(auditLogProvider);

        //var healthCheckProvider = serviceProvider.GetRequiredService<IHealthCheckProvider>();
        //Assert.NotNull(healthCheckProvider);

        //var permissionPusherProvider = serviceProvider.GetRequiredService<PermissionPusherProvider>();
        //Assert.NotNull(permissionPusherProvider);
    }
}