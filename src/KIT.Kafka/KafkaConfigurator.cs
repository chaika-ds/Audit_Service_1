using AuditService.Common.Consts;
using bgTeam.Extensions;
using FluentValidation;
using KIT.Kafka.BackgroundServices;
using KIT.Kafka.BackgroundServices.Runner.RunningRegistrar;
using KIT.Kafka.Consumers.AuditLog;
using KIT.Kafka.Consumers.AuditLog.Validators;
using KIT.Kafka.Consumers.BlockedPlayersLog;
using KIT.Kafka.Consumers.PlayerChangesLog;
using KIT.Kafka.HealthCheck;
using KIT.Kafka.Settings;
using KIT.Kafka.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Kafka;

namespace KIT.Kafka;

/// <summary>
///     Kafka configurator
/// </summary>
public static class KafkaConfigurator
{
    /// <summary>
    ///     Configure kafka. Register services and settings.
    /// </summary>
    /// <param name="services">Services сollection</param>
    /// <param name="environmentName">Host environment name</param>
    public static void ConfigureKafka(this IServiceCollection services, string environmentName)
    {
        var validationConsumerEnvironments = GetValidationConsumerEnvironments();

        services.AddKafkaSettings().AddKafkaServices().RegisterСonsumersRunner(
            configuration =>
            {
                configuration.Consumer<AuditLogConsumer>(settings =>
                {
                    settings.RunForEnvironments(validationConsumerEnvironments);
                });

                configuration.Consumer<BlockedPlayersLogConsumer>(settings =>
                {
                    settings.RunForEnvironments(validationConsumerEnvironments);
                });

                configuration.Consumer<PlayerChangesLogConsumer>(settings =>
                {
                    settings.RunForEnvironments(validationConsumerEnvironments);
                });

            }, environmentName);
    }
    
    /// <summary>
    ///     Add settings for working with kafka
    /// </summary>
    /// <param name="services">Services сollection</param>
    /// <returns>Services сollection</returns>
    private static IServiceCollection AddKafkaSettings(this IServiceCollection services)
    {
        services.AddSettings<IKafkaSettings, KafkaSettings>();
        services.AddSettings<IKafkaConsumerSettings, KafkaConsumerSettings>();
        services.AddSettings<IPermissionPusherSettings, PermissionPusherSettings>();
        services.AddSettings<ITopicValidationSettings, TopicValidationSettings>();
        services.AddSettings<IKafkaTopics, KafkaTopics>();
        return services;
    }

    /// <summary>
    ///     Add services for working with Kafka
    /// </summary>
    /// <param name="services">Services сollection</param>
    /// <returns>Services сollection</returns>
    private static IServiceCollection AddKafkaServices(this IServiceCollection services)
    {
        services.AddSingleton<IKafkaConsumerFactory, KafkaConsumerFactory>();
        services.AddSingleton<IKafkaProducer, KafkaProducer>();
        services.AddHostedService<PushPermissionService>();
        services.AddSingleton<IKafkaHealthCheck, KafkaHealthCheck>();
        services.AddValidatorsFromAssemblyContaining<AuditLogConsumerMessageValidator>(ServiceLifetime.Transient);
        return services;
    }

    /// <summary>
    ///     Get validation consumers environments
    /// </summary>
    /// <returns>Validation consumers environments</returns>
    private static string[] GetValidationConsumerEnvironments() =>
        new[]
        {
            EnvironmentNameConst.Debug,
            EnvironmentNameConst.Development,
            EnvironmentNameConst.Uat,
            EnvironmentNameConst.Test
        };
}