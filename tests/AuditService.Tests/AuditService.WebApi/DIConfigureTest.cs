﻿using AuditService.Handlers.PipelineBehaviors;
using AuditService.Setup.ModelProviders;
using AuditService.Tests.AuditService.WebApi.HttpClientMock;
using AuditService.Tests.AuditService.WebApi.Wrapper;
using AuditService.WebApi;
using bgTeam.Extensions;
using KIT.Kafka.BackgroundServices;
using KIT.Kafka.BackgroundServices.Runner;
using KIT.Kafka.HealthCheck;
using KIT.Redis.Settings;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Tolar.Authenticate;
using Tolar.Authenticate.Impl;
using Tolar.Kafka;
using Tolar.Redis;

namespace AuditService.Tests.AuditService.WebApi
{
    /// <summary>
    /// Web Api DI Configute test
    /// </summary>
    public class DIConfigureTest 
    {

        private class TestWrapper : IServiceCollectionExtensionsWrapper
        {
            public IServiceCollection AddSettings<TService, TImpl>(IServiceCollection services, Type[] tServices = null) where TService : class where TImpl : class, TService
            {
                return new ServiceCollection();
            }
        }

        private readonly ServiceCollectionMock _serviceCollectionMock;

        public DIConfigureTest()
        {
            _serviceCollectionMock = new ServiceCollectionMock();
        }

        private IServiceCollection ServiceCollection => _serviceCollectionMock.ServiceCollection;


        /// <summary>
        /// Testing RegisterServices Method
        /// </summary>
        [Fact]
        public async Task RegisterServices_ServicesInjection_Injected()
        {
            //var mockedIdentity = new Moq.Mock<IIdentity>();
            //mockedIdentity.Setup(x => x.Name).Returns("AdminUser");

            var services = new Mock<IServiceCollection>(MockBehavior.Loose);
            var serviceProvider = services.Object.BuildServiceProvider();
            var wrapper = new TestWrapper();
            var wm = new ServiceCollectionExtensionsWrapper(wrapper);
            //services.Setup(x => x.AddSettings<IRedisSettings, RedisSettings>())
            //    .Returns(wm.AddSettings<IRedisSettings, RedisSettings>(services.Object));

            //services.Object.RegisterServices("Debug");

            //_serviceCollectionMock._serviceCollectionMock.Setup(x =>
            //    x.AddHttpClient<IAuthenticateService, AuthenticateService>());
            //    //.Returns(new WrapperDefaultHttpClientBuilder(ServiceCollection, "")).Verifiable();
            var I = ServiceCollection.GetType().GetMethod("AddSettings");
            //I.SetValue(foo, 8675309);

            Mock.Get(ServiceCollection).Setup(x => x.AddHttpClient());



            DiConfigure.RegisterServices(this.ServiceCollection, "Debug");

            // ASSERT.
            _serviceCollectionMock.ContainsSingletonService<ITokenService, TokenService>();
            _serviceCollectionMock.ContainsTransientService<IApplicationModelProvider, ResponseHttpCodeModelProvider>();

            _serviceCollectionMock.ContainsSingletonService<IKafkaConsumerFactory, KafkaConsumerFactory>();
            _serviceCollectionMock.ContainsSingletonService<IKafkaProducer, KafkaProducer>();
            _serviceCollectionMock.ContainsSingletonService<IKafkaHealthCheck, KafkaHealthCheck>();

            //var serviceProvider1 = ServiceCollection.BuildServiceProvider();
            //var backgroundService = serviceProvider.GetService<IHostedService>() as PushPermissionService;
            //await backgroundService?.StartAsync(CancellationToken.None)!;
            //await backgroundService?.StopAsync(CancellationToken.None)!;

            //var backgroundService2 = serviceProvider.GetService<IHostedService>() as ConsumersRunner;
            //await backgroundService2?.StartAsync(CancellationToken.None)!;
            //await backgroundService2?.StopAsync(CancellationToken.None)!;

           // _serviceCollectionMock.ContainsScopedService<(typeof(IPipelineBehavior<,>), typeof(LogPipelineBehavior<,>))>();

            //typeof(IPipelineBehavior<,>), typeof(LogPipelineBehavior<,>)
            //typeof(IPipelineBehavior<,>), typeof(CachePipelineBehavior<,>)
            //var myHttpClient = serviceProvider.GetRequiredService<IAuthenticateService>();
            //var httpClient = Class1.GetAuthenticateServiceField(myHttpClient);


        }

    }
}

