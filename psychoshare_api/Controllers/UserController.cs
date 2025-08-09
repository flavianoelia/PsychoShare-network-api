using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.User;
using psychoshare_api.Services;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private static long _nextUserId = 1; // Static counter for consecutive IDs

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpPost("register")]
    public ActionResult<UserResponseDto> Register([FromBody] RegisterUserDto registerDto)
    {
        // TODO: Implement user registration logic
        // TODO: Validate input data
        // TODO: Check if email/username already exists
        // TODO: Hash password
        // TODO: Save to database
        // TODO: Return user response
        
        // Generate consecutive IDs for testing (1, 2, 3, 4, etc.)
        var userId = _nextUserId++;
        
        // Log user creation for easy identification
        _logger.LogInformation($"Created user: ID={userId}, Username={registerDto.Username}, Email={registerDto.Email}");
        
        var mockResponse = new UserResponseDto
        {
            IdPerson = userId,
            Name = registerDto.Name,
            LastName = registerDto.LastName,
            Username = registerDto.Username,
            Email = registerDto.Email,
            RoleName = "Psychologist",
            CreatedAt = DateTime.Now
        };
        
        return Ok(mockResponse);
    }

    [HttpPost("login")]
    public ActionResult<string> Login([FromBody] LoginUserDto loginDto)
    {
        // TODO: Implement user login logic
        // TODO: Validate credentials
        // TODO: Generate JWT token
        // TODO: Return authentication token
        
        return Ok("mock-jwt-token-12345");
    }

    [HttpGet("{id}")]
    public ActionResult<UserResponseDto> GetUser(long id)
    {
        // Try to get from pre-loaded test users first
        var testUser = MockDataService.Users.GetUserById(id);
        if (testUser != null)
        {
            _logger.LogInformation($"Retrieved test user: ID={id}, Username={testUser.Username}");
            return Ok(testUser);
        }
        
        // Fallback to mock response
        var mockUser = new UserResponseDto
        {
            IdPerson = id,
            Name = "Usuario",
            LastName = "Nuevo",
            Username = $"user_{id}",
            Email = $"user{id}@psychoshare.net",
            RoleName = "Psychologist",
            CreatedAt = DateTime.Now.AddDays(-7)
        };
        
        _logger.LogInformation($"Created mock user: ID={id}");
        return Ok(mockUser);
    }

    [HttpGet("search")]
    public ActionResult<List<UserResponseDto>> SearchUser([FromQuery] string? username = null, [FromQuery] string? email = null)
    {
        // Search in pre-loaded test users
        var results = MockDataService.Users.SearchUsers(username, email);
        
        _logger.LogInformation($"Search executed: username={username}, email={email}, found={results.Count}");
        return Ok(results);
    }

    [HttpGet]
    public ActionResult<List<UserResponseDto>> GetAllUsers()
    {
        // Return all pre-loaded test users
        var allUsers = MockDataService.Users.TestUsers;
        _logger.LogInformation($"� Retrieved all users: count={allUsers.Count}");
        return Ok(allUsers);
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
