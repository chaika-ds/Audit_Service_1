using Tolar.Redis;

namespace AuditService.Tests.Factories.Fakes;

/// <summary>
///      Fake redis reposetory for cache pipeline behavior
/// </summary>
internal class FakeRedisReposetoryForCachePipelineBehavior : IRedisRepository
{
    /// <summary>
    ///     stub for DecrementValueAsync
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public virtual Task<double> DecrementValueAsync(string key, double value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     stub for DeleteAsync to implement the interface IRedisRepository
    /// </summary>
    public async Task<bool> DeleteAsync(string key)
    {
        return true;
    }

    /// <summary>
    ///     stub for ExpireAsync to implement the interface IRedisRepository
    /// </summary>
    public async Task<bool> ExpireAsync(string key, TimeSpan? expiry)
    {
        return true;
    }

    /// <summary>
    ///     stub for GetAsync to implement the interface IRedisRepository
    /// </summary>
    public async Task<T?> GetAsync<T>(string key) where T : class
    {
        return null;
    }

    /// <summary>
    ///     stub for GetValueAsynс to implement the interface IRedisRepository
    /// </summary>
    public async Task<T?> GetValueAsync<T>(string key) where T : struct
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     stub for IncrementValueAsync to implement the interface IRedisRepository
    /// </summary>
    public Task<double> IncrementValueAsync(string key, double value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     stub for SetAsync to implement the interface IRedisRepository
    /// </summary>
    public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        return true;
    }
}

