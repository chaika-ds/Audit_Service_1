using AuditService.Common.Contexts;
using AuditService.Common.Models.Dto;
using AuditService.Localization;
using AuditService.Localization.Localizer.Storage;
using AuditService.Localization.Settings;
using AuditService.SettingsService.Commands.BaseEntities;
using AuditService.SettingsService.Commands.GetRootNodeTree;
using AuditService.Setup.AppSettings;
using AuditService.Tests.Fakes.Localization;
using AuditService.Tests.Fakes.Minio;
using AuditService.Tests.Fakes.SettingsService;
using AuditService.Tests.Fakes.Setup;
using AuditService.Tests.Fakes.Setup.ELK;
using AuditService.Tests.Fakes.Setup.Minio;
using bgTeam.Extensions;
using KIT.Kafka.HealthCheck;
using KIT.Minio;
using KIT.Minio.Commands.SaveFileWithSharing;
using KIT.Minio.Commands.SaveFileWithSharing.Models;
using KIT.Minio.HealthCheck;
using KIT.Minio.Settings.Interfaces;
using KIT.Redis;
using KIT.Redis.HealthCheck;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tolar.MinioService.Client;
using Tolar.Redis;
using static AuditService.Handlers.DiConfigure;

namespace AuditService.Tests.Fakes.ServiceData
{
    /// <summary>
    ///     Provider for fake service providers
    /// </summary>
    internal static class ServiceProviderFake
    {
        /// <summary>
        ///     Get service provider for log handlers
        /// </summary>
        /// <typeparam name="T">type of elk document</typeparam>
        /// <param name="jsonContent">json with content for elk in byte[] formate</param>
        /// <param name="index">elk index</param>
        /// <returns>Service provider</returns>
        internal static IServiceProvider GetServiceProviderForLogHandlers<T>(byte[] jsonContent, string index)
        {
            var services = new ServiceCollection();

            RegistrationServices(services);

            services.AddScoped(_ => ElasticSearchClientProviderFake.GetFakeElasticSearchClient<T>(jsonContent, index));

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        /// <summary>
        ///     Get service provider for reference request
        /// </summary>
        /// <returns>Service provider</returns>
        internal static IServiceProvider GetServiceProviderForLogHandlers()
        {
            var services = new ServiceCollection();

            RegistrationServices(services);

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        /// <summary>
        ///     Get service provider for minio
        /// </summary>
        /// <returns>Service provider</returns>
        internal static IServiceProvider GetServiceProviderForMinio()
        {
            var services = ServiceCollectionFake.CreateServiceCollectionFake();

            services.ConfigureMinio();

            services.AddSettings<IFileStorageSettings, MinioSettingsFake>();

            services.AddScoped<IMinioSharingFilesSettings, MinioSharingFilesSettingsFake>();

            services.AddLogging();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
        
        /// <summary>
        ///     Get service provider for Localization
        /// </summary>
        /// <returns>Service provider</returns>
        internal static IServiceProvider GetServiceProviderForLocalization()
        {
            var services = new ServiceCollection();

            services.ConfigureLocalization();

            RegistrationServices(services);

            services.AddSingleton<IRedisRepository, RedisReposetoryForCachePipelineBehaviorFake>();
            
            services.AddSingleton<IRedisCacheStorageSettings, RedisCacheStorageSettingsFake>();
            
            services.AddSingleton<ILocalizationSourceSettings, LocalizationSourceSettingsFake>();
            
            services.AddSingleton<ILocalizationStorage, RedisCacheStorageFake>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        /// <summary>
        ///     Get service provider for export log handlers
        /// </summary>
        /// <typeparam name="T">Type of elk document</typeparam>
        /// <param name="jsonContent">Json with content for elk in byte[] formate</param>
        /// <param name="index">Elk index</param>
        /// <returns>Service provider</returns>
        internal static IServiceProvider GetServiceProviderForExportLogHandlers<T>(byte[] jsonContent, string index)
        {
            var services = new ServiceCollection();

            RegistrationServices(services);

            services.AddScoped(_ => ElasticSearchClientProviderFake.GetFakeElasticSearchClient<T>(jsonContent, index));

            services.AddScoped<ISaveFileWithSharingCommand, SaveFileWithSharingCommandFake>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        /// <summary>
        ///     Get service provider for Health check handlers
        /// </summary>
        /// <param name="ritLabRequestHandler"></param>
        /// <param name="kafkaHcMock"></param>
        /// <param name="redisHcMock"></param>
        /// <param name="minioHealthCheckMock"></param>
        /// <returns>Service provider</returns>
        internal static IServiceProvider GetServiceProviderForHealthCheckHandlers(IRequestHandler<GitLabRequest, GitLabVersionResponseDto> ritLabRequestHandler,
            IKafkaHealthCheck kafkaHcMock, IRedisHealthCheck redisHcMock, IMinioHealthCheck minioHealthCheckMock)
        {
            var services = new ServiceCollection();

            RegistrationServices(services);

            services.AddScoped(_ => ElasticSearchClientProviderFake.GetFakeElasticSearchClient(""));
            services.AddScoped(_ => kafkaHcMock);
            services.AddScoped(_ => redisHcMock);
            services.AddScoped(_ => ritLabRequestHandler);
            services.AddScoped(_ => minioHealthCheckMock);

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        /// <summary>
        ///     Registration default services 
        /// </summary>
        /// <param name="services">service collection</param>
        private static void RegistrationServices(ServiceCollection services)
        {
            RegisterServices(services);
            services.AddSingleton<IRedisRepository, RedisReposetoryForCachePipelineBehaviorFake>();
            services.AddScoped<IElasticIndexSettings, ElasticSearchSettingsFake>();
            services.AddScoped<IGetRootNodeTreeCommand, GetRootNodeTreeCommandFake>();
            services.AddScoped<SettingsServiceCommands>();
            services.AddTransient(_ => new RequestContext
            {
                Language = "en", 
                XNodeId = Guid.NewGuid().ToString()
            });
            services.AddLogging();
        }
    }
}