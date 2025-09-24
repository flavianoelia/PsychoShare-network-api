using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.User;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private DAOFactory? df;
    public UserController(ILogger<UserController> logger, DAOFactory df)
    {
        _logger = logger;
        this.df = df;//agregado:inyectamos la factoría de DAOs
    }

   [HttpPost]
    public IActionResult Register([FromBody] CreateUserRequestDTO req)
    {
        return BadRequest();
    }

    [HttpGet("login")]

    //este controlador es un ejemplo y está incompleto
    public void Login()
    {
        // TODO: Implement user login
    }

    //este controlador es un ejemplo y está incompleto, obtiene un usuario por su id
    [HttpGet("{id}")]
    public void GetUser(long id)
    {
       // TODO: Implement user getUser
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