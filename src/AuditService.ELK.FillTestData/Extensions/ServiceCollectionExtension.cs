using AuditService.ELK.FillTestData.Generators;
using AuditService.Localization;
using AuditService.Setup;
using AuditService.Setup.ServiceConfigurations;
using KIT.Kafka;
using KIT.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.ELK.FillTestData.Extensions
{
    /// <summary>
    /// Extension of application services
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Register application services
        /// </summary>
        /// <param name="services">Services collection</param>
        public static void RegisterAppServices(this IServiceCollection services)
        {
            services.RegisterSettings();
            services.ConfigureLocalization();
            services.ConfigureRedis();
            services.ConfigureKafka(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")!);
            Handlers.DiConfigure.RegisterServices(services);
            services.AddScoped<VisitLogGenerator>();
            services.AddTransient<CategoryDictionary>();
            services.AddTransient<AuditLogDataGenerator>();
            services.AddTransient<BlockedPlayersLogDataGenerator>();
            services.AddTransient<PlayerChangesLogDataLogDataGenerator>();
            services.AddTransient<LossesLogGenerator>();

            services.AddElasticSearch();
        }
    }
}
