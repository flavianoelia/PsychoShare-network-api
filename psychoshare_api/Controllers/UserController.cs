using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("register")]
    public void Register()
    {
        // TODO: Implement user registration with EF Core
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


    [HttpGet("search/{username}")]
    public IActionResult SearchUser(string username)
    {
        // Mock search: returns a hardcoded user
        var user = new User { Username = username, Name = "Mock", LastName = "User", Email = "mock@user.com" };
        return Ok(new { success = true, content = user });
    }

    // GET /api/user/check-email?email=alguien@mail.com
    [HttpGet("check-email")]
    public IActionResult CheckEmail([FromQuery] string email)
    {
        var user = df?.DAOUser().GetUserByEmail(email);
        return Ok(new { exists = user != null });
    }
}