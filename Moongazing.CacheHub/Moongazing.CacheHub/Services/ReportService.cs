namespace Moongazing.CacheHub.Services;

public class ReportService : IReportService
{
    private readonly Dictionary<string, CacheReport> accessLogs = new();

    public Task RecordAccessAsync(string key)
    {
        if (accessLogs.TryGetValue(key, out var report))
        {
            report.AccessCount++;
            report.LastAccessed = DateTime.UtcNow;
        }
        else
        {
            accessLogs[key] = new CacheReport
            {
                Key = key,
                AccessCount = 1,
                LastAccessed = DateTime.UtcNow
            };
        }
        return Task.CompletedTask;
    }

    public Task<IEnumerable<CacheReport>> GetTopAccessedKeysAsync(int topN)
    {
        var topKeys = accessLogs.Values
            .OrderByDescending(r => r.AccessCount)
            .Take(topN);
        return Task.FromResult(topKeys);
    }
}