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

    // TODO: Add search, follow, and unfollow endpoints if needed
}