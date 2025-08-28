using Microsoft.AspNetCore.Mvc;

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
    public void Register()
    {
        // TODO: Implement user registration
    }

    [HttpGet("login")]
    //este controlador es un ejemplo y está incompleto
    public IActionResult Login()
    {
        //objeto User harcodeado para guardar algo en la "base de datos"
        User user = new User
        { Name = "Pepe", LastName = "Roldan", Email = "pepe.com", Password = "12345", Id = 1, Username = "pepeHD" };

        this.df.CreateDAOUser().Save(user);

        return Ok(new
        {
            success = true,
            content = user
        });
    }

    //este controlador es un ejemplo y está incompleto, obtiene un usuario por su id
    [HttpGet("{id}")]
    public IActionResult GetUser(long id)
    {
        User? user = this.df.CreateDAOUser().GetUser(id);
        return Ok(new
        {
            success = true,
            content = user
        });
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
