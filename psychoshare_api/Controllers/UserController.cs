using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.User;
using psychoshare_api.Services;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private DAOFactory df = new MockDAOFactory();

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterUserDto registerDto)
    {
        // Hardcoded User object for saving something in the "database"
        User user = new User
        { Name = registerDto.Name, LastName = registerDto.LastName, Email = registerDto.Email, Password = registerDto.Password, Username = registerDto.Username };

        this.df.CreateDAOUser().Save(user);

        return Ok(new
        {
            success = true,
            content = user
        });
    }

    [HttpGet("login")]
    // This controller is an example and is incomplete
    public IActionResult Login()
    {
        // Hardcoded User object for saving something in the "database"
        User user = new User
        { Name = "Pepe", LastName = "Roldan", Email = "pepe.com", Password = "12345", Username = "pepeHD" };

        this.df.CreateDAOUser().Save(user);

        return Ok(new
        {
            success = true,
            content = user
        });
 
    }

    [HttpGet("get/{id}")]
    public IActionResult GetUser(long id)
    {
        User? user = this.df.CreateDAOUser().GetUser(id);
        return Ok(new
        {
            success = true,
            content = user
        });
    }

    [HttpPut("edit/{id}")]
    public IActionResult EditProfile(int id)
    {
        // TODO: Edit user profile
        return Ok();
    }

    [HttpPost("follow")]
    public async Task<IActionResult> Follow([FromBody] psychoshare_api.DTOs.Following.CreateFollowingDto createFollowingDto, [FromServices] IFollowingService followingService)
    {
        var following = new Following
        {
            UserId = createFollowingDto.UserId,
            FollowedId = createFollowingDto.FollowedId,
            StartDate = DateTime.Now
        };
        var result = await followingService.CreateFollowingAsync(following);
        return Ok(new { success = true, content = result });
    }

    [HttpPost("unfollow")]
    public async Task<IActionResult> Unfollow([FromBody] psychoshare_api.DTOs.Following.CreateFollowingDto createFollowingDto, [FromServices] IFollowingService followingService)
    {
        var result = await followingService.DeleteFollowingAsync(createFollowingDto.UserId, createFollowingDto.FollowedId);
        return Ok(new { success = result });
    }

    [HttpGet("search/{username}")]
    public IActionResult SearchUser(string username)
    {
        // Mock search: returns a hardcoded user
        var user = new User { Username = username, Name = "Mock", LastName = "User", Email = "mock@user.com" };
        return Ok(new { success = true, content = user });
    }

    // TODO: Add search, follow, and unfollow endpoints if needed
}