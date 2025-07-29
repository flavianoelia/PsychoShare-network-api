using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpPost("register")]
    public void Register()
    {
        // TODO: Implement user registration
    }

    [HttpPost("login")]
    public void Login()
    {
        // TODO: Implement user login, return token
    }

    [HttpGet("{id}")]
    public void GetUser(int id)
    {
        // TODO: Get user by ID
    }

    [HttpPut("{id}")]
    public void EditProfile(int id)
    {
        // TODO: Edit user profile
    }

    [HttpGet("search")]
    public void SearchUser()
    {
        // TODO: Search users
    }

    [HttpPost("{userId}/follow/{targetUserId}")]
    public void FollowUser(int userId, int targetUserId)
    {
        // TODO: Follow user
    }

    [HttpDelete("{userId}/unfollow/{targetUserId}")]
    public void UnfollowUser(int userId, int targetUserId)
    {
        // TODO: Unfollow user
    }
}
