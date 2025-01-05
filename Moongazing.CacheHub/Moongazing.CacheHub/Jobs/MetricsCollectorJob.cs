using Moongazing.CacheHub.Services;

namespace Moongazing.CacheHub.Jobs;

public class MetricsCollectorJob
{
    private readonly IMetricsService metricsService
        ;

    public MetricsCollectorJob(IMetricsService metricsService)
    {
        this.metricsService = metricsService;
    }

    public async Task ExecuteAsync()
    {
        var metrics = await metricsService.GetMetricsAsync();
        Console.WriteLine($"Metrics: {metrics.Provider}, Total Keys: {metrics.TotalKeys}, Hits: {metrics.TotalHits}, Misses: {metrics.TotalMisses}");
    }
}