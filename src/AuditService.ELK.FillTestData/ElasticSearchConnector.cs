using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;

namespace AuditService.ELK.FillTestData;

/// <summary>
///     ELK connector
/// </summary>
public class ElasticSearchConnector
{
    /// <summary>
    ///     Create new instance for ELK
    /// </summary>
    public IElasticClient CreateInstance(IConfiguration configuration)
    {
        var uri = new Uri(configuration["ELASTIC_SEARCH:ELK_CONNECTION_URL"]);
        var pool = new SingleNodeConnectionPool(uri);
        var settings = new ConnectionSettings(pool);
        
        return new ElasticClient(settings);
    }
}