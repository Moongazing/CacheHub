using Moongazing.CacheHub.Services;

namespace Moongazing.CacheHub.Jobs
{
    public class CacheCleanupJob
    {
        private readonly ICacheService cacheService;

        public CacheCleanupJob(ICacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine("Running Cache Cleanup...");
            await cacheService.ClearAllAsync();
        }
    }
}
