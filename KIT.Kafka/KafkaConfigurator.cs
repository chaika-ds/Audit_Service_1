using bgTeam.Extensions;
using KIT.Kafka.BackgroundServices;
using KIT.Kafka.Settings;
using KIT.Kafka.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Kafka;

namespace KIT.Kafka
{
    /// <summary>
    /// Kafka configurator
    /// </summary>
    public static class KafkaConfigurator
    {
        /// <summary>
        /// Configure kafka. Register services and settings. 
        /// </summary>
        /// <param name="services">Services сollection</param>
        public static void ConfigureKafka(this IServiceCollection services)
        {
            services.AddKafkaSettings().AddKafkaServices();
        }

        /// <summary>
        /// Add settings for working with kafka
        /// </summary>
        /// <param name="services">Services сollection</param>
        /// <returns>Services сollection</returns>
        private static IServiceCollection AddKafkaSettings(this IServiceCollection services)
        {
            services.AddSettings<IKafkaSettings, KafkaSettings>();
            services.AddSettings<IPermissionPusherSettings, PermissionPusherSettings>();
            services.AddSettings<IKafkaTopics, KafkaTopics>();

            return services;
        }

        /// <summary>
        /// Add services for working with Kafka
        /// </summary>
        /// <param name="services">Services сollection</param>
        /// <returns>Services сollection</returns>
        private static IServiceCollection AddKafkaServices(this IServiceCollection services)
        {
            services.AddSingleton<IKafkaConsumerFactory, KafkaConsumerFactory>();
            services.AddSingleton<IKafkaProducer, KafkaProducer>();
            services.AddHostedService<PushPermissionBackgroundService>();

            return services;
        }
    }
}
