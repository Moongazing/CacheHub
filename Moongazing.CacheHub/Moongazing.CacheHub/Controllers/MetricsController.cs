using Microsoft.AspNetCore.Mvc;
using Moongazing.CacheHub.Services;

namespace Moongazing.CacheHub.Controllers;

[ApiController]
[Route("api/metrics")]
public class MetricsController : ControllerBase
{
    private readonly IMetricsService metricsService;

    public MetricsController(IMetricsService metricsService)
    {
        this.metricsService = metricsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMetrics()
    {
        var metrics = await metricsService.GetMetricsAsync();
        return Ok(metrics);
    }
}