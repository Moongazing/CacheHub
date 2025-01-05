public class CacheReport
{
    public string Key { get; set; } = default!;
    public int AccessCount { get; set; }
    public DateTime LastAccessed { get; set; }
}

