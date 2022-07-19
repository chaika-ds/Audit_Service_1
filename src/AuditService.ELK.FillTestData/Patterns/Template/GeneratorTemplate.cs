using AuditService.ELK.FillTestData.Models;
using Nest;
using Newtonsoft.Json;

namespace AuditService.ELK.FillTestData.Patterns.Template;

/// <summary>
///    Abstract Template model for Generators model
/// </summary>
internal abstract class GeneratorTemplate<TDtoModel, TResourceModel>
    where TDtoModel : class
    where TResourceModel : BaseModel
{
    private readonly IElasticClient _elasticClient;
    private string? _channelName;
    private TResourceModel? _config;

    /// <summary>
    ///  Initialize Generator Template
    /// </summary>
    protected GeneratorTemplate(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;

        Task.Run(async () =>
        {
            var data = GetResourceData();
            if (data.Length > 0)
            {
                _config = JsonConvert.DeserializeObject<TResourceModel>(System.Text.Encoding.Default.GetString(data));
            }

            await CleanBeforeAsync(_config);
            await GetAndCheckIndexAsync();
            await InsertAsync(_config);
        });
    }

    /// <summary>
    ///    Abstract method for getting channel name
    /// </summary>
    protected abstract string? GetChanelName();

    /// <summary>
    ///    Abstract method for getting resource data
    /// </summary>
    protected abstract byte[] GetResourceData();

    /// <summary>
    ///    Abstract method for inserting model to elk
    /// </summary>
    /// <param name="config">Configuration model</param>
    protected abstract Task InsertAsync(object? config);


    /// <summary>
    ///    Abstract method for cleaning data
    /// </summary>
    /// <param name="config">Configuration model</param>
    private async Task CleanBeforeAsync(BaseModel? config)
    {
        _channelName = GetChanelName();

        var cleanBefore = config!.CleanBefore;

        if (cleanBefore)
        {
            Console.WriteLine(@"Start force clean data");

            await _elasticClient.DeleteByQueryAsync<TDtoModel>(w =>
                w.Query(x => x.QueryString(q
                    => q.Query("*"))).Index(_channelName));

            await _elasticClient.Indices.DeleteAsync(_channelName);

            Console.WriteLine(@"Force clean has been comlpete!");
        }
    }

    /// <summary>
    ///    Abstract method for checking index
    /// </summary>
    private async Task GetAndCheckIndexAsync()
    {
        var index = await _elasticClient.Indices.ExistsAsync(_channelName);

        if (!index.Exists)
        {
            Console.WriteLine($@"Creating index {_channelName}");

            var response = await _elasticClient.Indices.CreateAsync(_channelName, r
                => r.Map<TDtoModel>(x => x.AutoMap()));

            if (!response.ShardsAcknowledged)
                throw response.OriginalException;

            Console.WriteLine(@"Index successfully created!");
        }
    }
}