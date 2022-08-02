using AuditService.Tests.Resources;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using System.Text;

namespace AuditService.Tests.Factories.Fakes;
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
    internal static IElasticClient GetFakeElasticSearchClient<T>(byte[] jsonContent, string elasticIndex)
    {
        var elkResponse = JsonConvert.DeserializeObject<List<T>>(Encoding.Default.GetString(jsonContent));

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

        var client = ClientBuilder(connection);

        return client;
    }

    /// <summary>
    ///     Getting fake elastic search client
    /// </summary>
    internal static IElasticClient GetFakeElasticSearchClient()
    {
        var connection = new InMemoryConnection();

        var client = ClientBuilder(connection);

        return client;
    }

    /// <summary>
    ///     Builder of fake elastic search client 
    /// </summary>
    private static IElasticClient ClientBuilder(InMemoryConnection connection)
    {
        var connectionPool = new SingleNodeConnectionPool(new Uri(fixedUri));

        var settings = new ConnectionSettings(connectionPool, connection).DefaultIndex(TestResources.DefaultIndex);

        return new ElasticClient(settings);
    }
}