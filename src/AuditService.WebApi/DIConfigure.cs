using Elasticsearch.Net;
using Nest;

namespace AuditService.WebApi;

public static class DiConfigure
{
    public static void Configure(IServiceCollection services)
    {
        services.AddScoped(typeof(IElasticClient), serviceProvider =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var uris = configuration.GetSection("ElasticSearch:Uris").Get<string[]>();
            var nodes = uris.Select(w => new Uri(w)).ToArray();

            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool);

            return new ElasticClient(settings);
        });
    }
}