using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.Following;
using psychoshare_api.Services;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class FollowingController : ControllerBase
{
    private readonly ILogger<FollowingController> _logger;
    private readonly IFollowingService _followingService;

    public FollowingController(ILogger<FollowingController> logger, IFollowingService followingService)
    {
        _logger = logger;
        _followingService = followingService;
    }

    [HttpPost]
    public async Task<ActionResult<FollowingResponseDto>> Follow([FromBody] CreateFollowingDto createFollowingDto)
    {
        // TODO: Implement follow user logic
        Following following = new Following
        {
            UserId = createFollowingDto.UserId,
            FollowedId = createFollowingDto.FollowedId,
            StartDate = DateTime.Now
        };

        var result = await _followingService.CreateFollowingAsync(following);
        
        var response = new FollowingResponseDto
        {
            FollowingId = result.FollowingId,
            UserId = result.UserId,
            FollowedId = result.FollowedId,
            StartDate = result.StartDate
        };

        return Ok(response);
    }

    [HttpDelete("{userId}/{followedId}")]
    public async Task<ActionResult<bool>> Unfollow(long userId, long followedId)
    {
        // TODO: Implement unfollow user logic
        var result = await _followingService.DeleteFollowingAsync(userId, followedId);
        return Ok(result);
    }

    [HttpGet("followers/{userId}")]
    public async Task<ActionResult<List<FollowingResponseDto>>> GetFollowers(long userId)
    {
        // TODO: Implement get user followers logic
        var followers = await _followingService.GetFollowersAsync(userId);
        
        var response = followers.Select(f => new FollowingResponseDto
        {
            FollowingId = f.FollowingId,
            UserId = f.UserId,
            FollowedId = f.FollowedId,
            StartDate = f.StartDate
        }).ToList();

        return Ok(response);
    }

    [HttpGet("following/{userId}")]
    public async Task<ActionResult<List<FollowingResponseDto>>> GetFollowing(long userId)
    {
        // TODO: Implement get users that user follows logic
        var following = await _followingService.GetFollowingAsync(userId);
        
        var response = following.Select(f => new FollowingResponseDto
        {
            FollowingId = f.FollowingId,
            UserId = f.UserId,
            FollowedId = f.FollowedId,
            StartDate = f.StartDate
        }).ToList();

        return Ok(response);
    }

    [HttpGet("check/{userId}/{targetUserId}")]
    public async Task<ActionResult<bool>> CheckFollowing(long userId, long targetUserId)
    {
        // TODO: Implement check if user follows target user logic
        var result = await _followingService.CheckIsFollowingAsync(userId, targetUserId);
        return Ok(result);
    }

    [HttpGet("followers/count/{userId}")]
    public async Task<ActionResult<int>> GetFollowersCount(long userId)
    {
        // TODO: Implement get followers count logic
        var result = await _followingService.GetFollowersCountAsync(userId);
        return Ok(result);
    }

    [HttpGet("following/count/{userId}")]
    public async Task<ActionResult<int>> GetFollowingCount(long userId)
    {
        // TODO: Implement get following count logic
        var result = await _followingService.GetFollowingCountAsync(userId);
        return Ok(result);
    }
}
