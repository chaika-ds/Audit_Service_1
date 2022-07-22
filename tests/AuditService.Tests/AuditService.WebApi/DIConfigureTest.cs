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
using AuditService.Tests.AuditService.WebApi.Fakes;
using KIT.Kafka;
using KIT.Kafka.BackgroundServices;
using KIT.Kafka.HealthCheck;
using KIT.Kafka.Settings;
using KIT.Kafka.Settings.Interfaces;
using KIT.Redis;
using KIT.Redis.HealthCheck;
using KIT.Redis.Settings;
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
using Tolar.Redis;
using static Xunit.Assert;
using DiConfigure = AuditService.Handlers.DiConfigure;
using GetCategoriesRequest = AuditService.Common.Models.Dto.GetCategoriesRequest;

namespace AuditService.Tests.AuditService.WebApi;

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
    public void ConfigureRedis_ServicesInjection_Injected()
    {
        //Act
        serviceCollectionFake.ConfigureRedis();

        // Assert
        IsRegisteredSettings<IRedisSettings>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredService<IRedisRepository, RedisRepository>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredService<IRedisHealthCheck, RedisHealthCheck>(serviceCollectionFake, ServiceLifetime.Singleton);
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
        IsRegisteredService<IMediator, Mediator>(serviceCollectionFake, ServiceLifetime.Transient);
        IsRegisteredService<IRequestHandler<
                LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto,
                    BlockedPlayersLogResponseDto>, PageResponseDto<BlockedPlayersLogResponseDto>>
            , BlockedPlayersLogRequestHandler>(serviceCollectionFake, ServiceLifetime.Transient);
        IsRegisteredService<IRequestHandler<CheckElkHealthRequest, bool>, HealthCheckRequestHandler>(serviceCollectionFake, ServiceLifetime.Transient);
        IsRegisteredService<IRequestHandler<CheckKafkaHealthRequest, bool>, HealthCheckRequestHandler>(serviceCollectionFake, ServiceLifetime.Transient);
        IsRegisteredService<IRequestHandler<CheckKafkaHealthRequest, bool>, HealthCheckRequestHandler>(serviceCollectionFake, ServiceLifetime.Transient);
        IsRegisteredService<IRequestHandler<CheckRedisHealthRequest, bool>, HealthCheckRequestHandler>(serviceCollectionFake, ServiceLifetime.Transient);

        IsRegisteredService<IRequestHandler<LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto,
                PlayerChangesLogResponseDto>,
            PageResponseDto<PlayerChangesLogResponseDto>>, PlayerChangesLogRequestHandler>(serviceCollectionFake, ServiceLifetime.Transient);
        IsRegisteredService<IRequestHandler<GetServicesRequest, IEnumerable<EnumResponseDto>>,
            ReferenceRequestHandler>(serviceCollectionFake, ServiceLifetime.Transient);
        IsRegisteredService<
            IRequestHandler<GetCategoriesRequest, IDictionary<ModuleName, CategoryDomainModel[]>>,
            ReferenceRequestHandler>(serviceCollectionFake, ServiceLifetime.Transient);
        IsRegisteredService<IRequestHandler<GetActionsRequest, IEnumerable<ActionDomainModel>?>,
            ReferenceRequestHandler>(serviceCollectionFake, ServiceLifetime.Transient);
        IsRegisteredService<IRequestHandler<GetEventsRequest, IDictionary<ModuleName, EventDomainModel[]>>,
            ReferenceRequestHandler>(serviceCollectionFake, ServiceLifetime.Transient);
        IsRegisteredService<IRequestHandler<
                LogFilterRequestDto<AuditLogFilterDto, LogSortDto, AuditLogTransactionDomainModel>,
                PageResponseDto<AuditLogTransactionDomainModel>>,
            AuditLogDomainRequestHandler>(serviceCollectionFake, ServiceLifetime.Transient);
        IsRegisteredService<IRequestHandler<
            LogFilterRequestDto<BlockedPlayersLogFilterDto,
                BlockedPlayersLogSortDto, BlockedPlayersLogDomainModel>,
            PageResponseDto<BlockedPlayersLogDomainModel>>, BlockedPlayersLogDomainRequestHandler>(serviceCollectionFake, ServiceLifetime.Transient);
        IsRegisteredService<IRequestHandler<
            LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogDomainModel>,
            PageResponseDto<PlayerChangesLogDomainModel>>, PlayerChangesLogDomainRequestHandler>(serviceCollectionFake, ServiceLifetime.Transient);
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
        IsRegisteredSettings<IKafkaSettings>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredSettings<IKafkaConsumerSettings>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredSettings<IPermissionPusherSettings>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredSettings<IKafkaTopics>(serviceCollectionFake, ServiceLifetime.Singleton);

        IsRegisteredService<IKafkaConsumerFactory, KafkaConsumerFactory>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredService<IKafkaProducer, KafkaProducer>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredService<IKafkaHealthCheck, KafkaHealthCheck>(serviceCollectionFake, ServiceLifetime.Singleton);

        IsRegisteredService<IHostedService, PushPermissionService>(serviceCollectionFake, ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Testing ConfigureRedis Method
    /// </summary>
    [Fact]
    public void RegisterSettings_SettingsInjection_Injected()
    {
        //Act
        serviceCollectionFake.RegisterSettings();

        // Assert
        IsRegisteredSettings<IAuthenticateServiceSettings>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredSettings<IAuthSsoServiceSettings>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredSettings<IElasticSearchSettings>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredSettings<IElasticIndexSettings>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredSettings<ISwaggerSettings>(serviceCollectionFake, ServiceLifetime.Singleton);
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
        IsRegisteredInternalService<IActionDescriptorCollectionProvider>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredService<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredInternalService<IApiDescriptionProvider>(serviceCollectionFake, ServiceLifetime.Transient);
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
        IsRegisteredInternalService<HealthCheckService>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredInternalService<IHostedService>(serviceCollectionFake, ServiceLifetime.Singleton);
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
        IsRegisteredInternalService<IElasticClient>(serviceCollectionFake, ServiceLifetime.Scoped);
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
        IsRegisteredService<ISwaggerProvider, SwaggerGenerator>(serviceCollectionFake, ServiceLifetime.Transient);
        IsRegisteredService<ISchemaGenerator, SchemaGenerator>(serviceCollectionFake, ServiceLifetime.Transient);
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
        IsRegisteredSettings<IRedisCacheStorageSettings>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredSettings<ILocalizationSourceSettings>(serviceCollectionFake, ServiceLifetime.Singleton);
        IsRegisteredInternalService<ILocalizationStorage>(serviceCollectionFake, ServiceLifetime.Scoped);
        IsRegisteredInternalService<ILocalizationSource>(serviceCollectionFake, ServiceLifetime.Scoped);
        IsRegisteredInternalService<ILocalizer>(serviceCollectionFake, ServiceLifetime.Scoped);
    }
}