using AuditService.Kafka.Services.Health;
using AuditService.Setup;
using AuditService.Setup.ServiceConfigurations;
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

            services.AddSingleton<HealthService>()
                .AddSingleton<IHealthService>(x => x.GetRequiredService<HealthService>());

            Handlers.DiConfigure.RegisterServices(services);
            services.AddScoped<CategoryDictionary>();
            services.AddScoped<ElasticSearchDataFiller>();
            services.AddElasticSearch();
        }
    }
}
