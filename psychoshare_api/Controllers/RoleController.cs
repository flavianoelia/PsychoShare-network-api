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

    private string GetRoleName(long? roleId)
    {
        return roleId switch
        {
            1 => "User",
            2 => "Admin",
            3 => "SuperAdmin",
            null => "No Role",
            _ => "Unknown"
        };
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

            var roleName = GetRoleName(user.RoleId);

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
                return StatusCode(403, "Only SuperAdmin can assign roles");

            if (request.RoleId < 1 || request.RoleId > 3)
                return BadRequest("Invalid RoleId. Must be 1 (User), 2 (Admin), or 3 (SuperAdmin)");

            var daoUser = _daoFactory.DAOUser();
            var user = daoUser.GetUser(userId);

            if (user == null)
                return NotFound($"User with ID {userId} not found");

            // Impedir asignar SuperAdmin a usuarios que no lo tienen ya en la BD
            if (request.RoleId == 3 && user.RoleId != 3)
                return BadRequest("No está permitido asignar el rol de SuperAdmin.");

            // Impedir que un SuperAdmin se quite su propio rol de SuperAdmin
            if (user.RoleId == 3 && request.RoleId != 3)
                return BadRequest("No puedes quitarte tu propio rol de SuperAdmin.");

            user.RoleId = request.RoleId;
            daoUser.UpdateUser(userId);

            var roleName = GetRoleName(request.RoleId);

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
