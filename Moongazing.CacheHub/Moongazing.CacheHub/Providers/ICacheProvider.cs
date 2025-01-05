namespace Moongazing.CacheHub.Providers;

public interface ICacheProvider
{
    Task SetAsync(string key, object value, TimeSpan expiration);
    Task<T> GetAsync<T>(string key);
    Task RemoveAsync(string key);
    Task ClearAllAsync();
    Task<int> GetTotalKeysAsync();
}
