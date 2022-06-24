using System.Text;
using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Setup.Extensions;
using AuditService.Utility.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Tolar.Redis;

namespace AuditService.Setup.Middleware;

public class RedisCacheMiddleware
{
    private const string JsonMediaType = "application/json";

    private readonly RequestDelegate _next;
    private readonly IRedisRepository _redis;
    
    public RedisCacheMiddleware(RequestDelegate next, IRedisRepository redis)
    {
        _next = next;
        _redis = redis;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var redisKey = context.Request.Path + context.Request.QueryString + context.Request.Headers["Token"];

        var checksum = redisKey.GetHash(HashType.MD5);

        var value = await _redis.GetAsync<string>(checksum);

        using var memStream = new MemoryStream();

        var originalBody = context.Response.Body;

        context.Response.ContentType = JsonMediaType;
        
        context.Response.Body = memStream;

        if (value == null)
        {
            await _next(context);

            memStream.Position = 0;

            using var streamReader = new StreamReader(memStream);

            var responseBody = await streamReader.ReadToEndAsync();

            await _redis.SetAsync(checksum, JsonConvert.SerializeObject(responseBody), TimeSpan.FromMinutes(10));
            
            memStream.Position = 0;
            
            await memStream.CopyToAsync(originalBody);
            
            context.Response.Body = originalBody;
            
        }
        else
        {
            var writer = new StreamWriter(memStream);

            await writer.WriteAsync(value);

            await writer.FlushAsync();
            
            memStream.Position = 0;

            await memStream.CopyToAsync(originalBody);

            context.Response.Body = memStream;
        }
    }
}