using AuditService.Common.Models.Domain.AuditLog;
using AuditService.Tests.AuditService.GetAuditLog.Models;
using AuditService.Tests.Resources;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using System.Text;

namespace AuditService.Tests.Factories.Fakes;
/// <summary>
/// Fake elastic search client provider
/// </summary>
internal static class FakeElasticSearchClientProvider
{
    const int fixedStatusCode = 200;

    const string fixedUri = "http://localhost:9200";

    /// <summary>
    /// Getting fake elastic search client
    /// </summary>
    internal static IElasticClient GetFakeElasticSearchClient<T>(byte[] jsonContent)
    {
        var elkResponse = JsonConvert
            .DeserializeObject<List<T>>(Encoding.Default.GetString(jsonContent));

        var response = new
        {
            hits = new
            {
                hits = Enumerable.Range(1, elkResponse.Count).Select(i => (object)new
                {
                    _index = TestResources.DefaultIndex,
                    _type = TestResources.DefaultIndex,
                    _id = $"{TestResources.DefaultIndex} {i}",
                    _score = 1.0,
                    _source = elkResponse[i - 1]
                }).ToArray()
            }
        };

        var responseBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
        var connection = new InMemoryConnection(responseBytes, fixedStatusCode);
        var connectionPool = new SingleNodeConnectionPool(new Uri(fixedUri));
        var settings = new ConnectionSettings(connectionPool, connection).DefaultIndex(TestResources.DefaultIndex);
        var client = new ElasticClient(settings);

        return client;
    }
}