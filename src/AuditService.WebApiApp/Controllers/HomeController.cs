using System.Text;
using AuditService.Data.Domain.Domain;
using AuditService.Common.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Nest;
using Tolar.Authenticate;
using AuditService.Utility.Logger;

namespace AuditService.WebApiApp.Controllers
{
    /// <summary>
    ///     Examlpes API for developers
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Obsolete("Заготовка для компонентов. Удалить до релиза")]
    public class HomeController : ControllerBase
    {
        private readonly IElasticClient _elasticClient;
        private readonly IDistributedCache _redisCache;
        private readonly IConfiguration _configuration;

        public HomeController(IElasticClient client, IDistributedCache redisCache, IConfiguration configuration)
        {
            _elasticClient = client;
            _redisCache = redisCache;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<string> BaseMethodAsync(int value)
        {
            await Task.Delay(1);
            Console.WriteLine($"New call. BaseMethod. Result = {value}");
            return $"BaseMethod. Result = {value}";
        }

        /// <summary>
        /// Get or set value with RedisCache
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(LoggingActionFilter))]
        [Authorize("AuditService.Home.exportValueToRedis")]
        [HttpGet]
        public async Task<string> GetOrSetStringWithRedisAsync(string value)
        {
            var valueCashe = await _redisCache.GetAsync(value);
            if (valueCashe != null)
                return Encoding.UTF8.GetString(valueCashe);

            await _redisCache.SetAsync(value, Encoding.UTF8.GetBytes($"MY CASHED DATA: {value}"));

            return $"DATA '{value}' HAS BEEN SAVING!";
        }

        /// <summary>
        /// GET all Audit log from Elastic search
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(LoggingActionFilter))]
        [Authorize("AuditService.Home.viewAuditLogFromElastic")]
        [HttpGet]
        public async Task<IList<AuditLogTransactionDomainModel>> GetAllFromElasticSearchAsync()
        {
            var count = await _elasticClient.CountAsync<AuditLogTransactionDomainModel>();
            var results = await _elasticClient.SearchAsync<AuditLogTransactionDomainModel>(s => s.Take((int)count.Count).Query(q => q.MatchAll()).Index(_configuration["ElasticSearch:DefaultIndex"]));
            return results.Documents.ToList();
        }

        /// <summary>
        /// GET Audit log from Elastic search by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(LoggingActionFilter))]
        [Authorize("AuditService.Home.viewAuditLogFromElastic")]
        [HttpGet]
        public async Task<AuditLogTransactionDomainModel> GetByIdFromElasticSearchAsync(string id)
        {
            var result = await _elasticClient.GetAsync<AuditLogTransactionDomainModel>(id, s => s.Index(_configuration["ElasticSearch:DefaultIndex"]));
            return result.Source;
        }

        /// <summary>
        /// PUT request to ElasticSearch for Audit log
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(LoggingActionFilter))]
        [Authorize("AuditService.Home.createAuditLogInElastic")]
        [HttpPost]
        public async Task<string> CreateInElasticSearchAsync(AuditLogTransactionDomainModel domainModel)
        {
            var result = await _elasticClient.CreateAsync(domainModel, s => s.Index(_configuration["ElasticSearch:DefaultIndex"]));
            return result.Id;
        }
    }
}