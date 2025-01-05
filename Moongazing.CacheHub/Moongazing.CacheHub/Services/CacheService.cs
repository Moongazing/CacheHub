using Moongazing.CacheHub.Providers;

namespace Moongazing.CacheHub.Services;

public class CacheService : ICacheService
{
    private readonly ICacheProvider cacheProvider;

    public CacheService(ICacheProvider cacheProvider)
    {
        this.cacheProvider = cacheProvider;
    }

    public Task SetAsync(string key, object value, TimeSpan expiration)
    {
        return cacheProvider.SetAsync(key, value, expiration);
    }

    public Task<T> GetAsync<T>(string key)
    {
        return cacheProvider.GetAsync<T>(key);
    }

    public Task RemoveAsync(string key)
    {
        return cacheProvider.RemoveAsync(key);
    }

    public Task ClearAllAsync()
    {
        return cacheProvider.ClearAllAsync();
    }
}