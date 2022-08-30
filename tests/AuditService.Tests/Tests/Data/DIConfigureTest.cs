using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.AuditLog;
using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Handlers;
using AuditService.Handlers.Handlers.DomainRequestHandlers;
using AuditService.Localization;
using AuditService.Localization.Localizer;
using AuditService.Localization.Localizer.Source;
using AuditService.Localization.Localizer.Storage;
using AuditService.Localization.Settings;
using AuditService.Setup;
using AuditService.Setup.AppSettings;
using AuditService.Setup.ServiceConfigurations;
using AuditService.Setup.ServiceConfigurations.Swagger;
using AuditService.Tests.Extensions;
using AuditService.Tests.Fakes.ServiceData;
using KIT.Kafka;
using KIT.Kafka.BackgroundServices;
using KIT.Kafka.HealthCheck;
using KIT.Kafka.Settings.Interfaces;
using KIT.Minio;
using KIT.Minio.Settings.Interfaces;
using KIT.Redis;
using KIT.Redis.HealthCheck;
using KIT.RocketChat;
using KIT.RocketChat.Settings.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Nest;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tolar.Authenticate.Impl;
using Tolar.Kafka;
using Tolar.MinioService.Client;
using Tolar.MinioService.Client.Impl;
using Tolar.Redis;
using DiConfigure = AuditService.Handlers.DiConfigure;
using GetCategoriesRequest = AuditService.Common.Models.Dto.GetCategoriesRequest;

namespace AuditService.Tests.Tests.Data;

/// <summary>
/// Web Api DI Configure test
/// </summary>
public class DIConfigureTest
{
    private IServiceCollection serviceCollectionFake;

    public DIConfigureTest()
    {
        serviceCollectionFake = ServiceCollectionFake.CreateServiceCollectionFake();
    }

    /// <summary>
    /// Testing ConfigureRedis Method
    /// </summary>
    [Fact]
    public void ConfigureMinio_ServicesInjection_Injected()
    {
        //Act
        serviceCollectionFake.ConfigureMinio();

        // Assert
        serviceCollectionFake.IsRegisteredSettings<IFileStorageSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredSettings<IMinioBucketSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredSettings<IMinioSharingFilesSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredService<IFileStorageService, MinioServiceClient>(ServiceLifetime.Transient);
    }

    /// <summary>
    /// Testing ConfigureRedis Method
    /// </summary>
    [Fact]
    public void ConfigureRedis_ServicesInjection_Injected()
    {
        //Act
        serviceCollectionFake.ConfigureRedis();

        // Assert
        serviceCollectionFake.IsRegisteredSettings<IRedisSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredService<IRedisRepository, RedisRepository>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredService<IRedisHealthCheck, RedisHealthCheck>(ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Testing ConfigureHandlers Method
    /// </summary>
    [Fact]
    public void ConfigureHandlers_ServicesInjection_Injected()
    {
        //Act
        DiConfigure.RegisterServices(serviceCollectionFake);

        // Assert
        serviceCollectionFake.IsRegisteredService<IMediator, Mediator>(ServiceLifetime.Transient);
        serviceCollectionFake.IsRegisteredService<IRequestHandler<
                LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto,
                    BlockedPlayersLogResponseDto>, PageResponseDto<BlockedPlayersLogResponseDto>>
            , BlockedPlayersLogRequestHandler>(ServiceLifetime.Transient);
        serviceCollectionFake.IsRegisteredService<IRequestHandler<CheckHealthRequest, HealthCheckResponseDto>, HealthCheckRequestHandler>(ServiceLifetime.Transient);

        serviceCollectionFake.IsRegisteredService<IRequestHandler<LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto,
                PlayerChangesLogResponseDto>,
            PageResponseDto<PlayerChangesLogResponseDto>>, PlayerChangesLogRequestHandler>(ServiceLifetime.Transient);
        serviceCollectionFake.IsRegisteredService<IRequestHandler<GetServicesRequest, IEnumerable<EnumResponseDto>>,
            ReferenceRequestHandler>(ServiceLifetime.Transient);
        serviceCollectionFake.IsRegisteredService<
            IRequestHandler<GetCategoriesRequest, IDictionary<ModuleName, CategoryDomainModel[]>>,
            ReferenceRequestHandler>(ServiceLifetime.Transient);
        serviceCollectionFake.IsRegisteredService<IRequestHandler<GetActionsRequest, IEnumerable<ActionDomainModel>?>,
            ReferenceRequestHandler>(ServiceLifetime.Transient);
        serviceCollectionFake.IsRegisteredService<IRequestHandler<GetEventsRequest, IDictionary<ModuleName, EventDomainModel[]>>,
            ReferenceRequestHandler>(ServiceLifetime.Transient);
        serviceCollectionFake.IsRegisteredService<IRequestHandler<
                LogFilterRequestDto<AuditLogFilterDto, LogSortDto, AuditLogDomainModel>,
                PageResponseDto<AuditLogDomainModel>>,
            AuditLogDomainRequestHandler>(ServiceLifetime.Transient);
        serviceCollectionFake.IsRegisteredService<IRequestHandler<
            LogFilterRequestDto<BlockedPlayersLogFilterDto,
                BlockedPlayersLogSortDto, BlockedPlayersLogDomainModel>,
            PageResponseDto<BlockedPlayersLogDomainModel>>, BlockedPlayersLogDomainRequestHandler>(ServiceLifetime.Transient);
        serviceCollectionFake.IsRegisteredService<IRequestHandler<
            LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogDomainModel>,
            PageResponseDto<PlayerChangesLogDomainModel>>, PlayerChangesLogDomainRequestHandler>(ServiceLifetime.Transient);
    }

    /// <summary>
    /// Testing RocketChatConfiguration
    /// </summary>
    [Fact]
    public void RocketChatConfiguration_ServicesInjection_Injected()
    {
        //Act
        serviceCollectionFake.ConfigureRocketChat();

        // Assert
        serviceCollectionFake.IsRegisteredSettings<IRocketChatApiSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredSettings<IRocketChatMethodsSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredSettings<IRocketChatStorageSettings>(ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Testing ConfigureKafka Method
    /// </summary>
    [Fact]
    public void ConfigureKafka_ServicesInjection_Injected()
    {
        //Act
        serviceCollectionFake.ConfigureKafka("TestEnvName");

        // Assert
        serviceCollectionFake.IsRegisteredSettings<IKafkaSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredSettings<IKafkaConsumerSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredSettings<IPermissionPusherSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredSettings<IKafkaTopics>(ServiceLifetime.Singleton);

        serviceCollectionFake.IsRegisteredService<IKafkaConsumerFactory, KafkaConsumerFactory>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredService<IKafkaProducer, KafkaProducer>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredService<IKafkaHealthCheck, KafkaHealthCheck>(ServiceLifetime.Singleton);

        serviceCollectionFake.IsRegisteredService<IHostedService, PushPermissionService>(ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Testing RegisterSettings Method
    /// </summary>
    [Fact]
    public void RegisterSettings_SettingsInjection_Injected()
    {
        //Act
        serviceCollectionFake.RegisterSettings();

        // Assert
        serviceCollectionFake.IsRegisteredSettings<IAuthenticateServiceSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredSettings<IAuthSsoServiceSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredSettings<IElasticSearchSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredSettings<IElasticIndexSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredSettings<ISwaggerSettings>(ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Testing ControllersServiceConfiguration
    /// </summary>
    [Fact]
    public void EndpointMetadataApiExplorerServiceCollectionExtensions_ExtensionsInjection_Injected()
    {
        //Act
        serviceCollectionFake.AddEndpointsApiExplorer();

        // Assert
        serviceCollectionFake.IsRegisteredInternalService<IActionDescriptorCollectionProvider>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredService<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredInternalService<IApiDescriptionProvider>(ServiceLifetime.Transient);
    }

    /// <summary>
    /// Testing HealthCheckServiceCollectionExtensions
    /// </summary>
    [Fact]
    public void HealthCheckServiceCollectionExtensions_ExtensionsInjection_Injected()
    {
        //Act
        serviceCollectionFake.AddHealthChecks();

        // Assert
        serviceCollectionFake.IsRegisteredInternalService<HealthCheckService>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredInternalService<IHostedService>(ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Testing ElasticSearchConfiguration
    /// </summary>
    [Fact]
    public void ElasticSearchConfiguration_ServicesInjection_Injected()
    {
        //Act
        serviceCollectionFake.AddElasticSearch();

        // Assert
        serviceCollectionFake.IsRegisteredInternalService<IElasticClient>(ServiceLifetime.Scoped);
    }

    /// <summary>
    /// Testing SwaggerConfiguration
    /// </summary>
    [Fact]
    public void SwaggerConfiguration_ServicesInjection_Injected()
    {
        //Act
        serviceCollectionFake.AddSwagger();

        // Assert
        serviceCollectionFake.IsRegisteredService<ISwaggerProvider, SwaggerGenerator>(ServiceLifetime.Transient);
        serviceCollectionFake.IsRegisteredService<ISchemaGenerator, SchemaGenerator>(ServiceLifetime.Transient);
    }

    /// <summary>
    /// Testing LocalizationConfigurator
    /// </summary>
    [Fact]
    public void LocalizationConfigurator_ServicesInjection_Injected()
    {
        //Act
        serviceCollectionFake.ConfigureLocalization();

        // Assert
        serviceCollectionFake.IsRegisteredSettings<IRedisCacheStorageSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredSettings<ILocalizationSourceSettings>(ServiceLifetime.Singleton);
        serviceCollectionFake.IsRegisteredInternalService<ILocalizationStorage>(ServiceLifetime.Scoped);
        serviceCollectionFake.IsRegisteredInternalService<ILocalizationSource>(ServiceLifetime.Scoped);
        serviceCollectionFake.IsRegisteredInternalService<ILocalizer>(ServiceLifetime.Scoped);
    }
}