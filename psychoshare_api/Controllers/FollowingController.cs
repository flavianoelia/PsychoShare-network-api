//using entity_library.following;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.Following;


namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class FollowingController : ControllerBase
{
    private readonly ILogger<FollowingController> _logger;
    private readonly AppDbContext _db;

    public FollowingController(ILogger<FollowingController> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }
}


/*
    [HttpPost]
    public async Task<ActionResult<FollowingResponseDto>> Follow([FromBody] CreateFollowingDto createFollowingDto)
    {
        var following = new Following
        {
            UserId = createFollowingDto.UserId,
            FollowedId = createFollowingDto.FollowedId,
            StartDate = DateTime.Now
        };
        _db.Followings.Add(following);
        await _db.SaveChangesAsync();
        var response = new FollowingResponseDto
        {
            FollowingId = following.FollowingId,
            UserId = following.UserId,
            FollowedId = following.FollowedId,
            StartDate = following.StartDate
        };
        return Ok(response);
    }

    [HttpDelete("{userId}/{followedId}")]
    public async Task<ActionResult<bool>> Unfollow(long userId, long followedId)
    {
        var following = await _db.Followings.FirstOrDefaultAsync(f => f.UserId == userId && f.FollowedId == followedId);
        if (following == null)
            return Ok(false);
        _db.Followings.Remove(following);
        await _db.SaveChangesAsync();
        return Ok(true);
    }

    [HttpGet("followers/{userId}")]
    public async Task<ActionResult<List<FollowingResponseDto>>> GetFollowers(long userId)
    {
        var followers = await _db.Followings.Where(f => f.FollowedId == userId).ToListAsync();
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
        var following = await _db.Followings.Where(f => f.UserId == userId).ToListAsync();
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
        var exists = await _db.Followings.AnyAsync(f => f.UserId == userId && f.FollowedId == targetUserId);
        return Ok(exists);
    }

    [HttpGet("followers/count/{userId}")]
    public async Task<ActionResult<int>> GetFollowersCount(long userId)
    {
    // ...existing code...
        var count = await _db.Followings.CountAsync(f => f.FollowedId == userId);
        return Ok(count);
    }

    [HttpGet("following/count/{userId}")]
    public async Task<ActionResult<int>> GetFollowingCount(long userId)
    {
        var count = await _db.Followings.CountAsync(f => f.UserId == userId);
        return Ok(count);
    }
}
*/