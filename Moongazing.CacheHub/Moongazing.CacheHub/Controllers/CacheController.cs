using Microsoft.AspNetCore.Mvc;
using Moongazing.CacheHub.Entites;
using Moongazing.CacheHub.Services;

namespace Moongazing.CacheHub.Controllers;

[ApiController]
[Route("api/cache")]
public class CacheController : ControllerBase
{
    private readonly ICacheService cacheService;

    public CacheController(ICacheService cacheService)
    {
        this.cacheService = cacheService;
    }

    [HttpPost("set")]
    public async Task<IActionResult> Set([FromBody] CacheItem item)
    {
        await cacheService.SetAsync(item.Key, item.Value, item.Expiration);
        return Ok();
    }

    [HttpGet("get/{key}")]
    public async Task<IActionResult> Get(string key)
    {
        var value = await cacheService.GetAsync<object>(key);
        return value != null ? Ok(value) : NotFound();
    }

    [HttpDelete("remove/{key}")]
    public async Task<IActionResult> Remove(string key)
    {
        await cacheService.RemoveAsync(key);
        return Ok();
    }

    [HttpPost("clear")]
    public async Task<IActionResult> Clear()
    {
        await cacheService.ClearAllAsync();
        return Ok();
    }
}