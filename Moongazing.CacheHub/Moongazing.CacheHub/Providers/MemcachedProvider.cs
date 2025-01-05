using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace Moongazing.CacheHub.Providers;

public class MemcachedProvider : ICacheProvider
{
    private readonly IMemcachedClient client;

    public MemcachedProvider(IMemcachedClient client)
    {
        this.client = client;
    }

    public async Task SetAsync(string key, object value, TimeSpan expiration)
    {
        await Task.Run(() =>
        {
            var success = client.Store(StoreMode.Set, key, value, DateTime.UtcNow.Add(expiration));
            if (!success)
            {
                throw new Exception($"Failed to set key '{key}' in Memcached.");
            }
        });
    }

    public async Task<T> GetAsync<T>(string key)
    {
        return await Task.Run(() =>
        {
            var value = client.Get<T>(key);
            if (value == null)
            {
                throw new KeyNotFoundException($"Key '{key}' not found in Memcached.");
            }
            return value;
        });
    }

    public async Task RemoveAsync(string key)
    {
        await Task.Run(() =>
        {
            var success = client.Remove(key);
            if (!success)
            {
                throw new Exception($"Failed to remove key '{key}' from Memcached.");
            }
        });
    }

    public async Task ClearAllAsync()
    {

        await Task.Run(() =>
        {
            var namespaceKey = "global_namespace";
            var currentNamespace = client.Get<string>(namespaceKey) ?? Guid.NewGuid().ToString();
            var newNamespace = Guid.NewGuid().ToString();

            var success = client.Store(StoreMode.Set, namespaceKey, newNamespace, DateTime.MaxValue);
            if (!success)
            {
                throw new Exception("Failed to reset global namespace in Memcached.");
            }
        });
    }
    public async Task<int> GetTotalKeysAsync()
    {
        throw new NotSupportedException("Memcached does not support retrieving all keys.");
    }
}