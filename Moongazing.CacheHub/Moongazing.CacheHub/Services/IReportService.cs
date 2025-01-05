namespace Moongazing.CacheHub.Services;

public interface IReportService
{
    Task<IEnumerable<CacheReport>> GetTopAccessedKeysAsync(int topN);
}