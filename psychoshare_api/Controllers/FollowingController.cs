using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class FollowingController : ControllerBase
{
    private readonly ILogger<FollowingController> _logger;

    public FollowingController(ILogger<FollowingController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public void Follow()
    {
        // TODO: Follow user
    }

    [HttpDelete("{userId}/{followedUserId}")]
    public void Unfollow(int userId, int followedUserId)
    {
        // TODO: Unfollow user
    }

    [HttpGet("followers/{userId}")]
    public void GetFollowers(int userId)
    {
        // TODO: Get user followers
    }

    [HttpGet("following/{userId}")]
    public void GetFollowing(int userId)
    {
        // TODO: Get users that user follows
    }

    [HttpGet("check/{userId}/{targetUserId}")]
    public void CheckFollowing(int userId, int targetUserId)
    {
        // TODO: Check if user follows target user
    }
}
