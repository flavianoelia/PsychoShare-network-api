using entity_library.following;
using entity_library.system;
using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.Following;
using psychoshare_api.DTOs.User;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class FollowingController : ControllerBase
{
    private readonly ILogger<FollowingController> _logger;
    private DAOFactory? df;

    public FollowingController(ILogger<FollowingController> logger, DAOFactory df)
    {
        _logger = logger;
        this.df = df;
    }

    [HttpPost]
    public ActionResult<FollowingResponseDto> Follow([FromBody] CreateFollowingDto createFollowingDto)
    {
        try
        {
            var following = new Following
            {
                UserId = createFollowingDto.UserId,
                FollowedId = createFollowingDto.FollowedUserId,
                StartDate = DateTime.Now
            };
            
            df!.DAOFollowing().Save(following);
            
            var response = new FollowingResponseDto
            {
                FollowingId = following.FollowingId,
                UserId = following.UserId,
                FollowedUserId = following.FollowedId,
                StartDate = following.StartDate
            };
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating following");
            return BadRequest("Error creating following relationship");
        }
    }

    [HttpDelete("{userId}/{followedUserId}")]
    public ActionResult<bool> Unfollow(long userId, long followedUserId)
    {
        try
        {
            DAOFactory? df = HttpContext.RequestServices.GetService(typeof(DAOFactory)) as DAOFactory;
            bool isFollowing = df!.DAOFollowing().CheckFollowing(userId, followedUserId);
            if (!isFollowing)
                return Ok(false);
                
            var following = new Following
            {
                UserId = userId,
                FollowedId = followedUserId
            };
            
            df!.DAOFollowing().Delete(following);
            return Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error unfollowing user");
            return BadRequest("Error removing following relationship");
        }
    }

    [HttpGet("followers/{userId}")]
    public ActionResult<List<UserResponseDto>> GetFollowers(long userId)
    {
        try
        {
            var followers = df!.DAOFollowing().GetFollowersFromUser(userId);
            var response = followers.Select(user => new UserResponseDto
            {
                IdPerson = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                CreatedAt = DateTime.Now
            }).ToList();
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting followers");
            return BadRequest("Error retrieving followers");
        }
    }

    [HttpGet("following/{userId}")]
    public ActionResult<List<UserResponseDto>> GetFollowing(long userId)
    {
        try
        {
            var following = df!.DAOFollowing().GetContactsFromUser(userId);
            var response = following.Select(user => new UserResponseDto
            {
                IdPerson = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                CreatedAt = DateTime.Now
            }).ToList();
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting following users");
            return BadRequest("Error retrieving following users");
        }
    }

    [HttpGet("check/{userId}/{targetUserId}")]
    public ActionResult<bool> CheckFollowing(long userId, long targetUserId)
    {
        try
        {
            var isFollowing = df!.DAOFollowing().CheckFollowing(userId, targetUserId);
            return Ok(isFollowing);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking following status");
            return BadRequest("Error checking following status");
        }
    }
}