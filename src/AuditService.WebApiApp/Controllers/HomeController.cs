﻿using System.Text;
using AuditService.Data.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Nest;

namespace AuditService.WebApiApp.Controllers
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
        public async Task<IList<AuditLogTransactionDto>> GetAllFromElasticSearchAsync()
        {
            var count = await _elasticClient.CountAsync<AuditLogTransactionDto>();
            var results = await _elasticClient.SearchAsync<AuditLogTransactionDto>(s => s.Take((int)count.Count).Query(q => q.MatchAll()).Index(_configuration["ElasticSearch:DefaultIndex"]));
            return results.Documents.ToList();
        }

        [HttpGet]
        public async Task<AuditLogTransactionDto> GetByIdFromElasticSearchAsync(string id)
        {
            var result = await _elasticClient.GetAsync<AuditLogTransactionDto>(id, s => s.Index(_configuration["ElasticSearch:DefaultIndex"]));
            return result.Source;
        }

        [HttpPost]
        public async Task<string> CreateInElasticSearchAsync(AuditLogTransactionDto dto)
        {
            var result = await _elasticClient.CreateAsync(dto, s => s.Index(_configuration["ElasticSearch:DefaultIndex"]));
            return result.Id;
        }
    }
}