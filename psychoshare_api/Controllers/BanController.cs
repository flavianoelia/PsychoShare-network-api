using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class BanController : ControllerBase
{
    private readonly ILogger<BanController> _logger;

    public BanController(ILogger<BanController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public void BanUser()
    {
        // TODO: Ban user with start/end date and reason
    }

    [HttpDelete("{userId}")]
    public void UnbanUser(int userId)
    {
        // TODO: Unban user
    }

    [HttpGet("{userId}")]
    public void GetUserBan(int userId)
    {
        // TODO: Get ban details for user
    }

    [HttpGet("active")]
    public void GetActiveBans()
    {
        // TODO: Get all active bans (admin only)
    }

    [HttpGet("check/{userId}")]
    public void CheckBanStatus(int userId)
    {
        // TODO: Check if user is currently banned
    }
}
