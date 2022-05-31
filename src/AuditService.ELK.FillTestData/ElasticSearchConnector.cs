using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace AuditService.ELK.FillTestData;

/// <summary>
///     Коннектор ЕЛК
/// </summary>
public class ElasticSearchConnector
{
    /// <summary>
    ///     Создать новый инстанс
    /// </summary>
    public IElasticClient CreateInstance(IServiceProvider services)
    {
        var configuration = services.GetRequiredService<IConfiguration>();

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
    }
}