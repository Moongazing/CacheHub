namespace Moongazing.CacheHub.Entites
{
    public class CacheItem
    {
        public string Key { get; set; } = default!;
        public object Value { get; set; } = default!;
        public TimeSpan Expiration { get; set; }
    }

}
