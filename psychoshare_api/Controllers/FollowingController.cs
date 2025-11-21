using entity_library.following;
using entity_library.system;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.Following;
using psychoshare_api.DTOs.User;
using System.Security.Claims;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FollowingController : ControllerBase
{
    private readonly ILogger<FollowingController> _logger;
    private DAOFactory? df;

    public FollowingController(ILogger<FollowingController> logger, DAOFactory df)
    {
        _logger = logger;
        this.df = df;
    }

    [HttpPost("{followedUserId}")]
    public ActionResult<FollowingResponseDto> Follow(long followedUserId)
    {
        try
        {
            // Extraer userId del JWT token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }
            long userId = long.Parse(userIdClaim.Value);

            // Validación: Un usuario no puede seguirse a sí mismo
            if (userId == followedUserId)
            {
                return BadRequest("A user cannot follow themselves");
            }

            // Validación: Verificar si ya está siguiendo al usuario
            bool alreadyFollowing = df!.DAOFollowing().CheckFollowing(userId, followedUserId);
            if (alreadyFollowing)
            {
                return BadRequest("User is already following this person");
            }

            var following = new Following
            {
                UserId = userId,
                FollowedId = followedUserId,
                StartDate = DateTime.Now
            };
            
            df!.DAOFollowing().Save(following);
            
            var response = new FollowingResponseDto
            {
                Id = following.Id,
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

    [HttpDelete("{followedUserId}")]
    public ActionResult<bool> Unfollow(long followedUserId)
    {
        try
        {
            // Extraer userId del JWT token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }
            long userId = long.Parse(userIdClaim.Value);

            // Validación: Un usuario no puede hacer unfollow de sí mismo
            if (userId == followedUserId)
            {
                return BadRequest("A user cannot unfollow themselves");
            }

            bool deleted = df!.DAOFollowing().DeleteByUserIds(userId, followedUserId);
            return Ok(deleted);
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
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email
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
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email
            }).ToList();
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting following users");
            return BadRequest("Error retrieving following users");
        }
    }

    [HttpGet("check/{targetUserId}")]
    public ActionResult<bool> CheckFollowing(long targetUserId)
    {
        try
        {
            // Extraer userId del JWT token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }
            long userId = long.Parse(userIdClaim.Value);

            var isFollowing = df!.DAOFollowing().CheckFollowing(userId, targetUserId);
            return Ok(isFollowing);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking following status");
            return BadRequest("Error checking following status");
        }
    }

    
    [HttpGet("followers/{userId}/count")]
    public ActionResult<int> GetFollowersCount(long userId)
    {
        try
        {
            var followers = df!.DAOFollowing().GetFollowersFromUser(userId);
            return Ok(followers.Count());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting followers count");
            return BadRequest("Error retrieving followers count");
        }
    }

    
    [HttpGet("following/{userId}/count")]
    public ActionResult<int> GetFollowingCount(long userId)
    {
        try
        {
            var following = df!.DAOFollowing().GetContactsFromUser(userId);
            return Ok(following.Count());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting following count");
            return BadRequest("Error retrieving following count");
        }
    }
}