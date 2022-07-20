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
using Microsoft.Extensions.Hosting;
using Tolar.Kafka;
using Tolar.Redis;
using DiConfigure = AuditService.Handlers.DiConfigure;

namespace AuditService.Tests.AuditService.WebApi;

/// <summary>
/// Web Api DI Configure test
/// </summary>
public class DIConfigureTest
{
    private ServiceCollectionFake serviceCollectionFake;

    public DIConfigureTest()
    {
        serviceCollectionFake = new ServiceCollectionFake();
    }

    /// <summary>
    /// Testing ConfigureRedis Method
    /// </summary>
    [Fact]
    public void ConfigureRedis_ServicesInjection_Injected()
    {
        //Act
        serviceCollectionFake.ServiceCollection.ConfigureRedis();

        // Assert
        serviceCollectionFake.ContainsSingletonService<IRedisSettings, RedisSettings>();
        serviceCollectionFake.ContainsSingletonService<IRedisRepository, RedisRepository>();
        serviceCollectionFake.ContainsSingletonService<IRedisHealthCheck, RedisHealthCheck>();
    }

    /// <summary>
    /// Testing ConfigureHandlers Method
    /// </summary>
    [Fact]
    public void ConfigureHandlers_ServicesInjection_Injected()
    {
        //Act
        DiConfigure.RegisterServices(serviceCollectionFake.ServiceCollection);

        // Assert
        serviceCollectionFake.ContainsTransientService<IMediator, Mediator>();
        serviceCollectionFake
            .ContainsTransientService<IRequestHandler<
                    LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto,
                        BlockedPlayersLogResponseDto>, PageResponseDto<BlockedPlayersLogResponseDto>>
                , BlockedPlayersLogRequestHandler>();
        serviceCollectionFake
            .ContainsScopedService<IRequestHandler<CheckElkHealthRequest, bool>, HealthCheckRequestHandler>();
        serviceCollectionFake
            .ContainsScopedService<IRequestHandler<CheckKafkaHealthRequest, bool>, HealthCheckRequestHandler>();
        serviceCollectionFake
            .ContainsScopedService<IRequestHandler<CheckRedisHealthRequest, bool>, HealthCheckRequestHandler>();
        serviceCollectionFake
            .ContainsTransientService<IRequestHandler<LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto,
                    PlayerChangesLogResponseDto>,
                PageResponseDto<PlayerChangesLogResponseDto>>, PlayerChangesLogRequestHandler>();
        serviceCollectionFake
            .ContainsTransientService<IRequestHandler<GetServicesRequest, IEnumerable<EnumResponseDto>>,
                ReferenceRequestHandler>();
        serviceCollectionFake
            .ContainsTransientService<
                IRequestHandler<GetCategoriesRequest, IDictionary<ModuleName, CategoryDomainModel[]>>,
                ReferenceRequestHandler>();
        serviceCollectionFake
            .ContainsTransientService<IRequestHandler<GetActionsRequest, IEnumerable<ActionDomainModel>?>,
                ReferenceRequestHandler>();
        serviceCollectionFake
            .ContainsTransientService<IRequestHandler<GetEventsRequest, IDictionary<ModuleName, EventDomainModel[]>>,
                ReferenceRequestHandler>();
        serviceCollectionFake
            .ContainsTransientService<IRequestHandler<
                    LogFilterRequestDto<AuditLogFilterDto, LogSortDto, AuditLogTransactionDomainModel>,
                    PageResponseDto<AuditLogTransactionDomainModel>>,
                AuditLogDomainRequestHandler>();
        serviceCollectionFake.ContainsTransientService<IRequestHandler<
            LogFilterRequestDto<BlockedPlayersLogFilterDto,
                BlockedPlayersLogSortDto, BlockedPlayersLogDomainModel>,
            PageResponseDto<BlockedPlayersLogDomainModel>>, BlockedPlayersLogDomainRequestHandler>();

        serviceCollectionFake.ContainsTransientService<IRequestHandler<
            LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogDomainModel>,
            PageResponseDto<PlayerChangesLogDomainModel>>, PlayerChangesLogDomainRequestHandler>();
    }

    /// <summary>
    /// Testing ConfigureKafka Method
    /// </summary>
    [Fact]
    public void ConfigureKafka_ServicesInjection_Injected()
    {
        //Act
        serviceCollectionFake.ServiceCollection.ConfigureKafka("TestEnvName");

        // Assert
        serviceCollectionFake.ContainsSingletonService<IKafkaSettings, KafkaSettings>();
        serviceCollectionFake.ContainsSingletonService<IKafkaConsumerSettings, KafkaConsumerSettings>();
        serviceCollectionFake.ContainsSingletonService<IPermissionPusherSettings, PermissionPusherSettings>();
        serviceCollectionFake.ContainsSingletonService<IKafkaTopics, KafkaTopics>();

        serviceCollectionFake.ContainsSingletonService<IKafkaConsumerFactory, KafkaConsumerFactory>();
        serviceCollectionFake.ContainsSingletonService<IKafkaProducer, KafkaProducer>();
        serviceCollectionFake.ContainsSingletonService<IKafkaHealthCheck, KafkaHealthCheck>();

        serviceCollectionFake.ContainsSingletonService<IHostedService, PushPermissionService>();
    }


}