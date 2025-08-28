using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;
    private readonly IReportService _reportService;

    public ReportController(ILogger<ReportController> logger, IReportService reportService)
    {
        _logger = logger;
        _reportService = reportService;
    }

    [HttpPost]
    public async Task<ActionResult<ReportResponseDto>> ReportUser([FromBody] CreateReportDto createReportDto)
    {
        // TODO: Implement report user logic
        var result = await _reportService.CreateReportAsync(createReportDto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<ReportResponseDto>>> GetAllReports()
    {
        // TODO: Implement get all reports logic
        var result = await _reportService.GetAllReportsAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReportResponseDto>> GetReport(long id)
    {
        // TODO: Implement get report logic
        var result = await _reportService.GetReportByIdAsync(id);
        if (result == null)
            return NotFound("Report not found");
        return Ok(result);
    }

    [HttpPut("{id}/resolve")]
    public async Task<ActionResult<bool>> ResolveReport(long id)
    {
        // TODO: Implement resolve report logic
        var result = await _reportService.ResolveReportAsync(id);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteReport(long id)
    {
        // TODO: Implement delete report logic
        var result = await _reportService.DeleteReportAsync(id);
        return Ok(result);
    }
}
