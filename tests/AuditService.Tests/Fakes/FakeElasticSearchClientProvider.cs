using AuditService.Common.Models.Domain.AuditLog;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using System.Text;

namespace AuditService.Tests.Fakes;

/// <summary>
///     Fake elastic search client provider
/// </summary>
internal static class FakeElasticSearchClientProvider
{
    const int fixedStatusCode = 200;

    const string fixedUri = "http://localhost:9200";

    /// <summary>
    ///     Getting fake elastic search client
    /// </summary>
    /// <typeparam name="T">type of elk document</typeparam>
    /// <param name="jsonContent">json with content for elk in byte[] formate</param>
    /// <param name="elasticIndex">elk index</param>
    /// <returns>Elastic client</returns>
    internal static IElasticClient GetFakeElasticSearchClient<T>(byte[] jsonContent, string elasticIndex)
    {
        var elkResponse = JsonConvert.DeserializeObject<List<T>>(Encoding.Default.GetString(jsonContent)) ?? new List<T>();

        var response = new
        {
            hits = new
            {
                hits = Enumerable.Range(1, elkResponse.Count).Select(i => (object)new
                {
                    _index = elasticIndex,
                    _type = elasticIndex,
                    _id = $"{elasticIndex} {i}",
                    _score = 1.0,
                    _source = elkResponse[i - 1]
                }).ToArray()
            }
        };

        var responseBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));

        var connection = new InMemoryConnection(responseBytes, fixedStatusCode);

        var client = ClientBuilder(connection, elasticIndex);

        return client;
    }

    /// <summary>
    ///     Getting fake elastic search client
    /// </summary>
    /// <param name="elasticIndex">elk index</param>
    /// <returns>Elastic client</returns>
    internal static IElasticClient GetFakeElasticSearchClient(string elasticIndex)
    {
        var connection = new InMemoryConnection();

        var client = ClientBuilder(connection, elasticIndex);

        return client;
    }


    /// <summary>
    ///     Builder of fake elastic search client 
    /// </summary>
    /// <param name="connection">elk in memory connection</param>
    /// <param name="elasticIndex">elk index</param>
    /// <returns>Elastic client</returns>
    private static IElasticClient ClientBuilder(InMemoryConnection connection, string elasticIndex)
    {
        var connectionPool = new SingleNodeConnectionPool(new Uri(fixedUri));

        var settings = new ConnectionSettings(connectionPool, connection)
            .DefaultIndex(elasticIndex)
            .DefaultFieldNameInferrer(s => s);

        return new ElasticClient(settings);
    }
}