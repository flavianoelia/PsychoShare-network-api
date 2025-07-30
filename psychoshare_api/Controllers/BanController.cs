using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class BanController : ControllerBase
{
    private readonly ILogger<BanController> _logger;
    private readonly IBanService _banService;

    public BanController(ILogger<BanController> logger, IBanService banService)
    {
        _logger = logger;
        _banService = banService;
    }

    [HttpPost]
    public async Task<ActionResult<BanResponseDto>> BanUser([FromBody] CreateBanDto createBanDto)
    {
        // TODO: Implement ban user logic
        var result = await _banService.CreateBanAsync(createBanDto);
        return Ok(result);
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult<bool>> UnbanUser(long userId)
    {
        // TODO: Implement unban user logic
        var result = await _banService.UnbanUserAsync(userId);
        return Ok(result);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<BanResponseDto>> GetUserBan(long userId)
    {
        // TODO: Implement get user ban logic
        var result = await _banService.GetBanByUserIdAsync(userId);
        if (result == null)
            return NotFound("Ban not found");
        return Ok(result);
    }

    [HttpGet("active")]
    public async Task<ActionResult<List<BanResponseDto>>> GetActiveBans()
    {
        // TODO: Implement get active bans logic
        var result = await _banService.GetAllActiveBansAsync();
        return Ok(result);
    }

    [HttpGet("check/{userId}")]
    public async Task<ActionResult<bool>> CheckBanStatus(long userId)
    {
        // TODO: Implement check ban status logic
        var result = await _banService.CheckUserIsBannedAsync(userId);
        return Ok(result);
    }
}
