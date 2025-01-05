using Prometheus;

namespace Moongazing.CacheHub.Monitoring;

public class PrometheusMetricsExporter
{
    private static readonly Counter TotalCacheHits = Metrics.CreateCounter("cache_hits_total", "Total number of cache hits.");
    private static readonly Counter TotalCacheMisses = Metrics.CreateCounter("cache_misses_total", "Total number of cache misses.");

    public void RecordHit()
    {
        TotalCacheHits.Inc();
    }

    public void RecordMiss()
    {
        TotalCacheMisses.Inc();
    }
}