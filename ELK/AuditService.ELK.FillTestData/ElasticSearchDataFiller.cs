using AuditService.Data.Domain.Dto;
using AuditService.Data.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Nest;
using Newtonsoft.Json;

namespace AuditService.ELK.FillTestData;

/// <summary>
///     Генератор тестовых данных в ЕЛК
/// </summary>
internal class ElasticSearchDataFiller
{
    private readonly IElasticClient _elasticClient;
    private readonly IConfiguration _configuration;
    private readonly CategoryDictionary _categoryDictionary;
    private readonly Random _random;

    public ElasticSearchDataFiller(IElasticClient elasticClient, IConfiguration configuration)
    {
        _elasticClient = elasticClient;
        _configuration = configuration;
        _random = new Random();
        _categoryDictionary = new CategoryDictionary(configuration);
    }

    /// <summary>
    ///     Начать генерацию
    /// </summary>
    public async Task Execute()
    {
        try
        {
            var cleanBefore = _configuration.GetValue<bool>("CleanBefore");
            if (cleanBefore)
            {
                Console.WriteLine("Запуск полной очистки");

                await _elasticClient.DeleteByQueryAsync<AuditLogTransactionDto>(w => w.Query(x => x.QueryString(q => q.Query("*"))));
                await _elasticClient.Indices.DeleteAsync(_configuration["ElasticSearch:DefaultIndex"]);

                Console.WriteLine("Полная очистка успешно завершена");
            }

            var index = await _elasticClient.Indices.ExistsAsync(_configuration["ElasticSearch:DefaultIndex"]);
            if (!index.Exists)
            {
                Console.WriteLine("Создание индекса " + _configuration["ElasticSearch:DefaultIndex"]);

                var response = await _elasticClient.Indices.CreateAsync(_configuration["ElasticSearch:DefaultIndex"], r => r.Map<AuditLogTransactionDto>(x => x.AutoMap()));
                if (!response.ShardsAcknowledged)
                    throw response.OriginalException;

                Console.WriteLine("Создание индекса успешно завершено");
            }

            Console.WriteLine("Получение конфигурации для генерации данных");
            
            var configurationModels = _configuration.GetSection("Fillers").Get<ConfigurationModel[]>();
            foreach (var configurationModel in configurationModels)
            {
                Console.WriteLine("");
                Console.WriteLine("Модель конфигурации:");
                Console.WriteLine(JsonConvert.SerializeObject(configurationModel, Formatting.Indented));
                
                var data = GenerateData(configurationModel);

                Console.WriteLine("Генерация завершена");

                foreach (var dto in data)
                    await _elasticClient.CreateAsync(dto, s => s.Index(_configuration["ElasticSearch:DefaultIndex"]).Id(dto.EntityId));

                Console.WriteLine("Сохранение завершено");
                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine("Все модели конфигурации были обработаны успешно");

            Console.WriteLine($"Всего было записано {configurationModels.Sum(w => w.Count)} записи.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Исключение: " + e);
        }
    }

    /// <summary>
    ///     Генератор данных
    /// </summary>
    /// <param name="configurationModel">Модель конфигурации</param>
    private IEnumerable<AuditLogTransactionDto> GenerateData(ConfigurationModel configurationModel)
    {
        for (var i = 0; i < configurationModel.Count; i++)
            yield return CreateNewDto(configurationModel);
    }

    /// <summary>
    ///     Создать модель Dto на основе конфигурации
    /// </summary>
    /// <param name="configurationModel">Модель конфигурации</param>
    private AuditLogTransactionDto CreateNewDto(ConfigurationModel configurationModel)
    {
        var dto = new AuditLogTransactionDto
        {
            NodeId = Guid.NewGuid(),
            ServiceName = configurationModel.ServiceName ?? Enum.GetValues<ServiceName>().GetRandomItem(_random),
            NodeType = configurationModel.NodeType ?? Enum.GetValues<NodeTypes>().GetRandomItem(_random),
            ActionName = configurationModel.ActionName ?? Enum.GetValues<ActionNameType>().GetRandomItem(_random),
            RequestUrl = "PUT: contracts/contractId?param=value",
            RequestBody = "{ 'myjson': 0 }",
            Timestamp = DateTime.Now.GetRandomItem(_random),
            EntityName = nameof(AuditLogTransactionDto),
            EntityId = Guid.NewGuid(),
            OldValue = "{ 'value': '0' }",
            NewValue = "{ 'value': '1' }",
            ProjectId = Guid.NewGuid(),
            User = new IdentityUserDto
            {
                Id = Guid.NewGuid(),
                Ip = "127.0.0.0",
                Login = "login",
                UserAgent = "agent"
            }
        };

        dto.CategoryCode = string.IsNullOrEmpty(configurationModel.CategoryCode)
            ? _categoryDictionary.GetCategory(dto.ServiceName, _random)
            : configurationModel.CategoryCode;

        return dto;
    }
}