using AuditService.Setup.AppSettings;
using Elasticsearch.Net;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace AuditService.Setup.ServiceConfigurations;

/// <summary>
///     Configuration of ELK
/// </summary>
public static class ElasticSearchConfiguration
{
    /// <summary>
    ///     Create scope for ElasticSearch
    /// </summary>
    public static void AddElasticSearch(this IServiceCollection services)
    {
        services.AddScoped<IElasticClient>(serviceProvider =>
        {
            var configuration = serviceProvider.GetRequiredService<IElasticSearchSettings>();
            if (string.IsNullOrEmpty(configuration.ConnectionUrl))
                throw new ArgumentException($"{nameof(configuration.ConnectionUrl)} is null");

            var uri = new Uri(configuration.ConnectionUrl);
            var pool = new SingleNodeConnectionPool(uri);
            var settings = new ConnectionSettings(pool)
                .ThrowExceptions();

            if (!string.IsNullOrEmpty(configuration.UserName))
                settings.BasicAuthentication(configuration.UserName, configuration.Password);

            return new ElasticClient(settings);
        });
    }
}