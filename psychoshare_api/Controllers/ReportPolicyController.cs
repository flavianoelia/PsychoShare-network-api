using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.Report;
using psychoshare_api.DTOs.Ban;
using psychoshare_api.Services;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportPolicyController : ControllerBase
{
    private readonly ILogger<ReportPolicyController> _logger;
    private readonly IReportService _reportService;
    private readonly IBanService _banService;

    public ReportPolicyController(ILogger<ReportPolicyController> logger, IReportService reportService, IBanService banService)
    {
        _logger = logger;
        _reportService = reportService;
        _banService = banService;
    }

    // Report endpoints
    [HttpPost("report")]
    public async Task<ActionResult<ReportResponseDto>> ReportUser([FromBody] CreateReportDto createReportDto)
    {
        var result = await _reportService.CreateReportAsync(createReportDto);
        return Ok(result);
    }

    [HttpGet("reports")]
    public async Task<ActionResult<List<ReportResponseDto>>> GetAllReports()
    {
        var result = await _reportService.GetAllReportsAsync();
        return Ok(result);
    }

    [HttpGet("report/{id}")]
    public async Task<ActionResult<ReportResponseDto>> GetReport(long id)
    {
        var result = await _reportService.GetReportByIdAsync(id);
        if (result == null)
            return NotFound("Report not found");
        return Ok(result);
    }

    [HttpPut("report/{id}/resolve")]
    public async Task<ActionResult<bool>> ResolveReport(long id)
    {
        var result = await _reportService.ResolveReportAsync(id);
        return Ok(result);
    }

    [HttpDelete("report/{id}")]
    public async Task<ActionResult<bool>> DeleteReport(long id)
    {
        var result = await _reportService.DeleteReportAsync(id);
        return Ok(result);
    }

    // Ban endpoints
    [HttpPost("ban")]
    public async Task<ActionResult<BanResponseDto>> BanUser([FromBody] CreateBanDto createBanDto)
    {
        var result = await _banService.CreateBanAsync(createBanDto);
        return Ok(result);
    }

    [HttpDelete("ban/{userId}")]
    public async Task<ActionResult<bool>> UnbanUser(long userId)
    {
        var result = await _banService.UnbanUserAsync(userId);
        return Ok(result);
    }

    [HttpGet("ban/{userId}")]
    public async Task<ActionResult<BanResponseDto>> GetUserBan(long userId)
    {
        var result = await _banService.GetBanByUserIdAsync(userId);
        if (result == null)
            return NotFound("Ban not found");
        return Ok(result);
    }

    [HttpGet("bans/active")]
    public async Task<ActionResult<List<BanResponseDto>>> GetActiveBans()
    {
        var result = await _banService.GetAllActiveBansAsync();
        return Ok(result);
    }

    [HttpGet("ban/check/{userId}")]
    public async Task<ActionResult<bool>> CheckBanStatus(long userId)
    {
        var result = await _banService.CheckUserIsBannedAsync(userId);
        return Ok(result);
    }
}
