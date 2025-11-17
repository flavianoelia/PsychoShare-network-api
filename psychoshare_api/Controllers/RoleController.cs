using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RoleController : ControllerBase
{
    private readonly ILogger<RoleController> _logger;
    private readonly DAOFactory _daoFactory;

    public RoleController(ILogger<RoleController> logger, DAOFactory daoFactory)
    {
        _logger = logger;
        _daoFactory = daoFactory;
    }

    [HttpGet]
    public IActionResult GetAllRoles()
    {
        try
        {
            var roles = new[]
            {
                new { Id = 1, Name = "User" },
                new { Id = 2, Name = "Admin" },
                new { Id = 3, Name = "SuperAdmin" }
            };

            return Ok(roles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all roles");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetUserRole(long userId)
    {
        try
        {
            var daoUser = _daoFactory.DAOUser();
            var user = daoUser.GetUser(userId);

            if (user == null)
                return NotFound($"User with ID {userId} not found");

            var roleName = user.RoleId switch
            {
                1 => "User",
                2 => "Admin",
                3 => "SuperAdmin",
                null => "No Role",
                _ => "Unknown"
            };

            return Ok(new { RoleId = user.RoleId ?? 0, RoleName = roleName });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user role for user {UserId}", userId);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("user/{userId}")]
    public IActionResult AssignRole(long userId, [FromBody] AssignRoleRequest request)
    {
        try
        {
            var roleClaimValue = User.FindFirst(ClaimTypes.Role)?.Value;
            long currentUserRoleId;
            if (!long.TryParse(roleClaimValue, out currentUserRoleId))
                currentUserRoleId = 1;

            if (currentUserRoleId != 3)
                return Forbid("Only SuperAdmin can assign roles");

            if (request.RoleId < 1 || request.RoleId > 3)
                return BadRequest("Invalid RoleId. Must be 1 (User), 2 (Admin), or 3 (SuperAdmin)");

            var daoUser = _daoFactory.DAOUser();
            var user = daoUser.GetUser(userId);

            if (user == null)
                return NotFound($"User with ID {userId} not found");

            user.RoleId = request.RoleId;
            daoUser.UpdateUser(userId);

            var roleName = request.RoleId switch
            {
                1 => "User",
                2 => "Admin",
                3 => "SuperAdmin",
                _ => "Unknown"
            };

            return Ok(new { Message = $"Role updated successfully to {roleName}", RoleId = request.RoleId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error assigning role to user {UserId}", userId);
            return StatusCode(500, "Internal server error");
        }
    }
}

public class AssignRoleRequest
{
    public long RoleId { get; set; }
}
