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
            services.ConfigureRedis();
            services.ConfigureKafka(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")!);
            Handlers.DiConfigure.RegisterServices(services);
            services.AddScoped<CategoryDictionary>();
            services.AddScoped<AuditLogGenerator>();
            services.AddElasticSearch();
        }
    }
}
