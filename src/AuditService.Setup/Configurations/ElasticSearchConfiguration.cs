using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace AuditService.Setup.Configurations;

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
        services.AddScoped(typeof(IElasticClient), serviceProvider =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var uri = new Uri(configuration["ELASTIC_SEARCH:ELK_CONNECTION_URL"]);
            var pool = new SingleNodeConnectionPool(uri);
            var settings = new ConnectionSettings(pool);

            return new ElasticClient(settings);
        });
    }
}