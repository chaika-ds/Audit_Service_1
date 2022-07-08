using AuditService.Common.Attributes;
using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using MediatR;
using Newtonsoft.Json;
using Tolar.Redis;

namespace AuditService.Handlers.PipelineBehaviors;

/// <summary>
///     Handler pipeline element for request caching
/// </summary>
/// <typeparam name="TRequest">Request type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam>
public class CachePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : class
{
    private readonly IRedisRepository _redisRepository;

    public CachePipelineBehavior(IRedisRepository redisRepository)
    {
        _redisRepository = redisRepository;
    }

    /// <summary>
    ///     Handle the request and apply caching if necessary
    /// </summary>
    /// <param name="request">Request for caching</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="next">The request handling pipeline delegate</param>
    /// <returns>Response after request handling</returns>
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (GetCacheAttribute() is not UseCacheAttribute cacheAttribute)
            return await next();

        var cacheKey = GenerateCacheKey(request, cacheAttribute);
        var response = await _redisRepository.GetAsync<TResponse>(cacheKey);

        if (response is not null)
            return response;

        response = await next();
        await _redisRepository.SetAsync(cacheKey, response, TimeSpan.FromSeconds(cacheAttribute.Lifetime));
        return response;
    }

    /// <summary>
    ///     Generate a key to store the cache
    /// </summary>
    /// <param name="request">Request Model</param>
    /// <param name="useCacheAttribute">Attribute for working with cache</param>
    /// <returns>Key to store the cache</returns>
    private static string? GenerateCacheKey(TRequest request, UseCacheAttribute useCacheAttribute)
    {
        var masterCacheKey = !string.IsNullOrEmpty(useCacheAttribute.Key)
            ? useCacheAttribute.Key
            : request.GetType().Name;

        var json = JsonConvert.SerializeObject(request);
        var key = $"{masterCacheKey}--{json}";
        return key.GetHash(HashType.MD5);
    }

    /// <summary>
    ///     Get the cache attribute
    /// </summary>
    /// <returns>Cache attribute</returns>
    private object? GetCacheAttribute()
        => typeof(TRequest).GetCustomAttributes(typeof(UseCacheAttribute), false).FirstOrDefault();
}