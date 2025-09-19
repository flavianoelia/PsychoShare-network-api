using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.User;
using psychoshare_api.Services;

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
    public IActionResult Register([FromBody] RegisterUserDto registerDto)
    {
        // TODO: Implement user registration with EF Core
        return Ok(new { success = false, message = "Not implemented" });
    }

    [HttpGet("login")]
    // This controller is an example and is incomplete
    public IActionResult Login()
    {
        // TODO: Implement login logic with EF Core
        return Ok(new { success = false, message = "Not implemented" });
    }

    [HttpGet("get/{id}")]
    public IActionResult GetUser(long id)
    {
        // TODO: Implement get user by id with EF Core
        return Ok(new { success = false, message = "Not implemented" });
    }

    [HttpPut("edit/{id}")]
    public IActionResult EditProfile(int id)
    {
        // TODO: Edit user profile
        return Ok();
    }

    // [HttpPost("follow")]
    // public async Task<IActionResult> Follow([FromBody] psychoshare_api.DTOs.Following.CreateFollowingDto createFollowingDto)
    // {
    //     // TODO: Implement follow logic using EF Core
    //     return Ok(new { success = false, message = "Not implemented" });
    // }

    // [HttpPost("unfollow")]
    // public async Task<IActionResult> Unfollow([FromBody] psychoshare_api.DTOs.Following.CreateFollowingDto createFollowingDto)
    // {
    //     // TODO: Implement unfollow logic using EF Core
    //     return Ok(new { success = false, message = "Not implemented" });
    // }

    [HttpGet("search/{username}")]
    public IActionResult SearchUser(string username)
    {
        // Mock search: returns a hardcoded user
        var user = new User { Username = username, Name = "Mock", LastName = "User", Email = "mock@user.com" };
        return Ok(new { success = true, content = user });
    }

    // TODO: Add search, follow, and unfollow endpoints if needed
}