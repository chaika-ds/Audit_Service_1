using AuditService.Kafka.AppSetings;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Kafka;

namespace AuditService.Kafka
{
    /// <summary>
    ///    Kafka registry settings
    /// </summary>
    public static class KafkaSettings
    {
        /// <summary>
        ///     Register kafka consumer app settings by sections
        /// </summary>
        public static void AddKafkaSettings(this IServiceCollection services)
        {
            services.AddSettings<IKafkaConsumerSettings, KafkaConsumerSettings>();
            services.AddSettings<IHealthSettings, HealthSettings>();
        }
    }
}
