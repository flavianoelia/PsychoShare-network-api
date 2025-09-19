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
        // TODO: Implement user registration
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
