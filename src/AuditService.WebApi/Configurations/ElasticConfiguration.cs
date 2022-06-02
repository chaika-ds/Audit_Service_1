using AuditService.Common.ELK;
using Elasticsearch.Net;
using Nest;

namespace AuditService.WebApi.Configurations;

public static class ElasticConfiguration
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IElasticClient), serviceProvider =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var uris = configuration.GetSection("ElasticSearch:Uris").Get<string[]>();
            var nodes = uris.Select(w => new Uri(w)).ToArray();

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
        
        services.Configure<ElasticOptions>(configuration.GetSection("ElasticSearch"));
    }
}