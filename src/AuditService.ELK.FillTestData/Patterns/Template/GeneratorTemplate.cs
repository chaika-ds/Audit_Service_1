using AuditService.ELK.FillTestData.Models;
using Nest;
using Newtonsoft.Json;

namespace AuditService.ELK.FillTestData.Patterns.Template;

internal abstract class GeneratorTemplate<TModel, TResourceModel>
    where TModel : class
    where TResourceModel : BaseModel
{
    private readonly IElasticClient _elasticClient;
    private string? _channelName;

    protected GeneratorTemplate(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;

        Task.Run(async () =>
        {
            var config = JsonConvert.DeserializeObject<TResourceModel>(System.Text.Encoding.Default.GetString(GetResourceData()));
            if (config != null)
            {
                await CleanBeforeAsync(config);
                await GetAndCheckIndexAsync();
                await InsertAsync(config);
            }
        });
    }

    protected abstract string? GetChanelName();
    protected abstract byte[] GetResourceData();
    protected abstract Task InsertAsync(object config);


    private async Task CleanBeforeAsync(BaseModel? config)
    {
        _channelName = GetChanelName();

        var cleanBefore = config!.CleanBefore;

        if (cleanBefore)
        {
            Console.WriteLine(@"Start force clean data");

            await _elasticClient.DeleteByQueryAsync<TModel>(w =>
                w.Query(x => x.QueryString(q
                    => q.Query("*"))).Index(_channelName));

            await _elasticClient.Indices.DeleteAsync(_channelName);

            Console.WriteLine(@"Force clean has been comlpete!");
        }
    }

    private async Task GetAndCheckIndexAsync()
    {
        var index = await _elasticClient.Indices.ExistsAsync(_channelName);

        if (!index.Exists)
        {
            Console.WriteLine($@"Creating index {_channelName}");

            var response = await _elasticClient.Indices.CreateAsync(_channelName, r
                => r.Map<TModel>(x => x.AutoMap()));

            if (!response.ShardsAcknowledged)
                throw response.OriginalException;

            Console.WriteLine(@"Index successfully created!");
        }
    }
}