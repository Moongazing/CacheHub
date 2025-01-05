using StackExchange.Redis;
using System.Text.Json;

namespace Moongazing.CacheHub.Providers;

public class RedisCacheProvider : ICacheProvider
{
    private readonly IConnectionMultiplexer redis;

    public RedisCacheProvider(IConnectionMultiplexer redis)
    {
        this.redis = redis;
    }

    public async Task SetAsync(string key, object value, TimeSpan expiration)
    {
        var db = redis.GetDatabase();
        var jsonValue = JsonSerializer.Serialize(value);
        await db.StringSetAsync(key, jsonValue, expiration);
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var db = redis.GetDatabase();
        var value = await db.StringGetAsync(key);
        return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value);
    }

    public async Task RemoveAsync(string key)
    {
        var db = redis.GetDatabase();
        await db.KeyDeleteAsync(key);
    }

    public async Task ClearAllAsync()
    {
        var endpoints = redis.GetEndPoints();
        foreach (var endpoint in endpoints)
        {
            var server = redis.GetServer(endpoint);
            await server.FlushDatabaseAsync();
        }
    }
    public async Task<int> GetTotalKeysAsync()
    {
        var db = redis.GetDatabase();
        var endpoints = redis.GetEndPoints();
        var totalKeys = 0;

        foreach (var endpoint in endpoints)
        {
            var server = redis.GetServer(endpoint);
            var keys = server.Keys();
            totalKeys += keys.Count();
        }

        return await Task.FromResult(totalKeys);
    }
}
