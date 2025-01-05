using Moongazing.CacheHub.Entites;
using Moongazing.CacheHub.Providers;

namespace Moongazing.CacheHub.Services;

public class MetricsService : IMetricsService
{
    private long totalHits = 0;
    private long totalMisses = 0;
    private readonly ICacheProvider cacheProvider;

    public MetricsService(ICacheProvider cacheProvider)
    {
        this.cacheProvider = cacheProvider;
    }

    public async Task<CacheMetrics> GetMetricsAsync()
    {
        int totalKeys;
        try
        {
            totalKeys = await cacheProvider.GetTotalKeysAsync();
        }
        catch (NotSupportedException)
        {
            totalKeys = -1;
        }

        var metrics = new CacheMetrics
        {
            Provider = cacheProvider.GetType().Name,
            TotalKeys = totalKeys,
            TotalHits = totalHits,
            TotalMisses = totalMisses,
            Timestamp = DateTime.UtcNow
        };
        return metrics;
    }

    public Task RecordHitAsync()
    {
        Interlocked.Increment(ref totalHits);
        return Task.CompletedTask;
    }

    public Task RecordMissAsync()
    {
        Interlocked.Increment(ref totalMisses);
        return Task.CompletedTask;
    }
}