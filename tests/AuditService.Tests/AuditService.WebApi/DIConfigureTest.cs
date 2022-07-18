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
using AuditService.Handlers.PipelineBehaviors;
using AuditService.WebApi;
using KIT.Kafka;
using KIT.Kafka.BackgroundServices;
using KIT.Kafka.HealthCheck;
using KIT.Kafka.Settings;
using KIT.Kafka.Settings.Interfaces;
using KIT.Redis;
using KIT.Redis.HealthCheck;
using KIT.Redis.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
    private ServiceCollectionMock serviceCollectionMock;

    public DIConfigureTest()
    {
        serviceCollectionMock = new ServiceCollectionMock();
    }

    /// <summary>
    /// Testing ConfigureRedis Method
    /// </summary>
    [Fact]
    public void ConfigureRedis_ServicesInjection_Injected()
    {
        //Act
        serviceCollectionMock.ServiceCollection.ConfigureRedis();

        // Assert
        serviceCollectionMock.ContainsSingletonService<IRedisSettings, RedisSettings>();
        serviceCollectionMock.ContainsSingletonService<IRedisRepository, RedisRepository>();
        serviceCollectionMock.ContainsSingletonService<IRedisHealthCheck, RedisHealthCheck>();
    }

    /// <summary>
    /// Testing ConfigureHandlers Method
    /// </summary>
    [Fact]
    public void ConfigureHandlers_ServicesInjection_Injected()
    {
        //Act
        DiConfigure.RegisterServices(serviceCollectionMock.ServiceCollection);

        // Assert
        serviceCollectionMock.ContainsTransientService<IMediator, Mediator>();
        serviceCollectionMock
            .ContainsTransientService<IRequestHandler<
                    LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto,
                        BlockedPlayersLogResponseDto>, PageResponseDto<BlockedPlayersLogResponseDto>>
                , BlockedPlayersLogRequestHandler>();
        serviceCollectionMock
            .ContainsScopedService<IRequestHandler<CheckElkHealthRequest, bool>, HealthCheckRequestHandler>();
        serviceCollectionMock
            .ContainsScopedService<IRequestHandler<CheckKafkaHealthRequest, bool>, HealthCheckRequestHandler>();
        serviceCollectionMock
            .ContainsScopedService<IRequestHandler<CheckRedisHealthRequest, bool>, HealthCheckRequestHandler>();
        serviceCollectionMock
            .ContainsTransientService<IRequestHandler<LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto,
                    PlayerChangesLogResponseDto>,
                PageResponseDto<PlayerChangesLogResponseDto>>, PlayerChangesLogRequestHandler>();
        serviceCollectionMock
            .ContainsTransientService<IRequestHandler<GetServicesRequest, IEnumerable<EnumResponseDto>>,
                ReferenceRequestHandler>();
        serviceCollectionMock
            .ContainsTransientService<
                IRequestHandler<GetCategoriesRequest, IDictionary<ModuleName, CategoryDomainModel[]>>,
                ReferenceRequestHandler>();
        serviceCollectionMock
            .ContainsTransientService<IRequestHandler<GetActionsRequest, IEnumerable<ActionDomainModel>?>,
                ReferenceRequestHandler>();
        serviceCollectionMock
            .ContainsTransientService<IRequestHandler<GetEventsRequest, IDictionary<ModuleName, EventDomainModel[]>>,
                ReferenceRequestHandler>();
        serviceCollectionMock
            .ContainsTransientService<IRequestHandler<
                    LogFilterRequestDto<AuditLogFilterDto, LogSortDto, AuditLogTransactionDomainModel>,
                    PageResponseDto<AuditLogTransactionDomainModel>>,
                AuditLogDomainRequestHandler>();
        serviceCollectionMock.ContainsTransientService<RequestHandler<
            LogFilterRequestDto<BlockedPlayersLogFilterDto,
                BlockedPlayersLogSortDto, BlockedPlayersLogDomainModel>,
            PageResponseDto<BlockedPlayersLogDomainModel>>, BlockedPlayersLogDomainRequestHandler>();
        serviceCollectionMock.ContainsTransientService<RequestHandler<
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
        serviceCollectionMock.ServiceCollection.ConfigureKafka("TestEnvName");

        // Assert
        serviceCollectionMock.ContainsSingletonService<IKafkaSettings, KafkaSettings>();
        serviceCollectionMock.ContainsSingletonService<IKafkaConsumerSettings, KafkaConsumerSettings>();
        serviceCollectionMock.ContainsSingletonService<IPermissionPusherSettings, PermissionPusherSettings>();
        serviceCollectionMock.ContainsSingletonService<IKafkaTopics, KafkaTopics>();

        serviceCollectionMock.ContainsSingletonService<IKafkaConsumerFactory, KafkaConsumerFactory>();
        serviceCollectionMock.ContainsSingletonService<IKafkaProducer, KafkaProducer>();
        serviceCollectionMock.ContainsSingletonService<IKafkaHealthCheck, KafkaHealthCheck>();

        serviceCollectionMock.ContainsSingletonService<IHostedService, PushPermissionService>();
    }


}