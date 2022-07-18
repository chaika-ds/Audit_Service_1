using AuditService.Tests.AuditService.WebApi;
using AuditService.Tests.AuditService.WebApi.HttpClientMock;
using AuditService.WebApi;
using KIT.Redis;
using KIT.Redis.HealthCheck;
using KIT.Redis.Settings;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Authenticate;
using Tolar.Redis;

namespace AuditService.Tests.KIT.Redis
{
    public class RedisConfiguratorTest
    {
        private readonly ServiceCollectionMock _serviceCollectionMock;

        public RedisConfiguratorTest()
        {
            _serviceCollectionMock = new ServiceCollectionMock();
        }

        private IServiceCollection ServiceCollection => _serviceCollectionMock.ServiceCollection;

        /// <summary>
        /// Testing RegisterServices Method
        /// </summary>
        [Fact]
        public void ConfigureRedis_ServicesInjection_Injected()
        {
            //var mockedIdentity = new Moq.Mock<IIdentity>();
            //mockedIdentity.Setup(x => x.Name).Returns("AdminUser");
            //Act
            this.ServiceCollection.ConfigureRedis();

            // ASSERT.
            _serviceCollectionMock.ContainsSingletonService<IRedisSettings, RedisSettings>();
            _serviceCollectionMock.ContainsSingletonService<RedisRepository, RedisRepository>();
            _serviceCollectionMock.ContainsSingletonService<IRedisHealthCheck, RedisHealthCheck>();
            AddRedisCacheTest();
        }

        private void AddRedisCacheTest()
        {
            var serviceProvider = ServiceCollection.BuildServiceProvider();
            var settings = serviceProvider.GetRequiredService<IRedisSettings>();
        }
    }
}
