using Microsoft.AspNetCore.Mvc;
using Moongazing.CacheHub.Services;

[ApiController]
[Route("api/reports")]
public class ReportController : ControllerBase
{
    private readonly IReportService reportService;

    public ReportController(IReportService reportService)
    {
        this.reportService = reportService;
    }

    [HttpGet("top-accessed")]
    public async Task<IActionResult> GetTopAccessedKeys([FromQuery] int topN = 10)
    {
        var reports = await reportService.GetTopAccessedKeysAsync(topN);
        return Ok(reports);
    }
}
