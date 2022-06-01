using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DockerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHost;

        public DockerController(IConfiguration configuration, IWebHostEnvironment webHost)
        {
            _configuration = configuration;
            _webHost = webHost;
        }

        [HttpGet]
        public async Task<string> BaseMethodAsync(string value)
        {
            await Task.Delay(1);
            Console.WriteLine($"New call. BaseMethod: {value}. Result = {value}");

            Console.WriteLine("REDISINFO");
            Console.WriteLine(_configuration["RedisCache:ConnectionString"]);

            Console.WriteLine("DIRECTORIES");
            Console.WriteLine(string.Join("\r\n", Directory.GetDirectories(_webHost.ContentRootPath)));
            Console.WriteLine("FILES");
            Console.WriteLine(string.Join("\r\n", Directory.GetFiles(_webHost.ContentRootPath)));
            
            return $"BaseMethod: {_webHost.ContentRootPath}. Result = {value}";
        }
    }
}
