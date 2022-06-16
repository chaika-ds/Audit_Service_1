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

            var uris = configuration.GetSection("ElasticSearch").GetChildren();
            var nodes = uris.Select(w => new Uri(w.Value)).ToArray();

            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool);

            var defaultIndex = configuration["ElasticSearch:DefaultIndex"];
            if (!string.IsNullOrEmpty(defaultIndex))
                settings.DefaultIndex(defaultIndex);

            var userName = configuration["ElasticSearch:UserName"];
            var password = configuration["ElasticSearch:Password"];
            if (!string.IsNullOrEmpty(userName))
                settings.BasicAuthentication(userName, password);

            return new ElasticClient(settings);
        });
    }
}