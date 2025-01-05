using Moongazing.CacheHub.Entites;

namespace Moongazing.CacheHub.Services;

public interface IMetricsService
{
    Task<CacheMetrics> GetMetricsAsync();
    Task RecordHitAsync();
    Task RecordMissAsync();
}