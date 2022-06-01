using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Nest;

namespace AuditService.WebApi.Controllers
{
    /// <summary>
    ///     Examlpes API for developers
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly IElasticClient _elasticClient;
        private readonly IDistributedCache _redisCache;
        private readonly IWebHostEnvironment _webHost;
        private readonly IConfiguration _configuration;

        public HomeController(IElasticClient client, IDistributedCache redisCache, IWebHostEnvironment webHost, IConfiguration configuration)
        {
            _elasticClient = client;
            _redisCache = redisCache;
            _webHost = webHost;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<string> BaseMethodAsync(int value)
        {
            await Task.Delay(1);
            Console.WriteLine($"New call. BaseMethod: {value}. Result = {value}");

            Console.WriteLine(string.Join("\r\n", Directory.GetDirectories(_webHost.ContentRootPath)));
            Console.WriteLine(_configuration["RedisCache:ConnectionString"]);

            return $"BaseMethod: {_webHost.ContentRootPath}. Result = {value}";
        }

        [HttpGet]
        public async Task<string> GetOrSetStringWithRedisAsync(string value)
        {
            var valueCashe = await _redisCache.GetAsync(value);
            if (valueCashe != null)
                return Encoding.UTF8.GetString(valueCashe);

            await _redisCache.SetAsync(value, Encoding.UTF8.GetBytes($"MY CASHED DATA: {value}"));

            return $"DATA '{value}' HAS BEEN SAVING!";
        }

        [HttpGet]
        public async Task<IList<BookObject>> GetAllFromElasticSearchAsync()
        {
            var results = await _elasticClient.SearchAsync<BookObject>(s => s.Query(q => q.MatchAll()).Index("book"));
            return results.Documents.ToList();
        }

        [HttpGet]
        public async Task<BookObject> GetByIdFromElasticSearchAsync(string id)
        {
            var result = await _elasticClient.GetAsync<BookObject>(id, s => s.Index("book"));
            return result.Source;
        }

        [HttpGet]
        public async Task<string> CreateInElasticSearchAsync(string name, int page)
        {
            var result = await _elasticClient.CreateAsync(new BookObject { Id = Guid.NewGuid(), Name = name, PageCount = page }, s => s.Index("book"));
            return result.Id;
        }
    }

    public class BookObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PageCount { get; set; }
    }
}