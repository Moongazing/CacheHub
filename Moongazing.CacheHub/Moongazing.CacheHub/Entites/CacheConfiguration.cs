namespace Moongazing.CacheHub.Entites
{
    public class CacheConfiguration
    {
        public string Provider { get; set; } = default!;
        public int DefaultExpirationMinutes { get; set; }
    }

}
