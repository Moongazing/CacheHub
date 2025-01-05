namespace Moongazing.CacheHub.Entites
{
    public class CacheMetrics
    {
        public string Provider { get; set; } = default!;
        public int TotalKeys { get; set; }
        public long TotalHits { get; set; }
        public long TotalMisses { get; set; }
        public DateTime Timestamp { get; set; }
    }

}

