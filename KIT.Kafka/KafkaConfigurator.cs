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
            services.AddSettings<IKafkaSettings, KafkaSettings>();
            services.AddSettings<IPermissionPusherSettings, PermissionPusherSettings>();
            services.AddSettings<IKafkaTopics, KafkaTopics>();

            services.AddSingleton<IKafkaConsumerFactory, KafkaConsumerFactory>();
            services.AddSingleton<IKafkaProducer, KafkaProducer>();
            services.AddHostedService<PushPermissionBackgroundService>();
        }
    }
}
