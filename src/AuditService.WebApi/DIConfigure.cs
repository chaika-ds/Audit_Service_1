using Elasticsearch.Net;
using Nest;

namespace AuditService.WebApi;

public static class DiConfigure
{
    public static IServiceCollection Configure(IServiceCollection services, IConfiguration ñonfiguration)
    {
        RegisterElasticSearch(services, ñonfiguration);

        return services;
    }

    /// <summary>
    ///     Çàðåãèñòðèðîâàòü ElasticSearch
    /// </summary>
    private static void RegisterElasticSearch(IServiceCollection services, IConfiguration configuration)
    {
        var nodes = configuration.GetSection("ElasticSearch:Uris").Get<string[]>().Select(w => new Uri(w)).ToArray();
        if (!nodes.Any())
            return;

        services.AddScoped(typeof(IElasticClient), _ =>
        {
            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool);

            return new ElasticClient(settings);
        });
    }
}