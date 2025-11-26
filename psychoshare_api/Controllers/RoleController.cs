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
            var roles = Enum.GetValues<RoleType>()
            
                .Select(r => new
                {
                    Id = (int)r,
                    Name = r.ToString()
                });
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
            var user = _daoFactory.DAOUser().GetUser(userId);

            if (user == null)
                return NotFound($"User with ID {userId} not found");

            return Ok(new 
            { 
                RoleId = (int)user.RoleType, 
                RoleName = user.RoleType.ToString() 
            });
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
            // get logged user rol
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

            if (!Enum.TryParse<RoleType>(roleClaim, out var currentUserRole))
                currentUserRole = RoleType.User;
            
            // Only superadmin can assing roles 
            if (currentUserRole != RoleType.Superadmin)
                return StatusCode(403, "Only SuperAdmin can assign roles");
            
            // Validation of roltype
            if (!Enum.IsDefined(typeof(RoleType), request.RoleType))
                return BadRequest("Invalid role");
            
            var user = _daoFactory.DAOUser().GetUser(userId);

            if (user == null)
                return NotFound($"User with ID {userId} not found");

              // Reglas especiales
            if (request.RoleType == RoleType.Superadmin && user.RoleType != RoleType.Superadmin)
                return BadRequest("No está permitido asignar el rol de SuperAdmin.");

            if (user.RoleType == RoleType.Superadmin && request.RoleType != RoleType.Superadmin)
                return BadRequest("No puedes quitarte tu propio rol de SuperAdmin.");

            user.RoleType = request.RoleType;
            _daoFactory.DAOUser().UpdateUser(user.Id);


            return Ok(new
            {
                Message = $"Role updated successfully to {user.RoleType}",
                RoleId = (int)user.RoleType
            });
            }
            catch (Exception ex)
            {
            _logger.LogError(ex, "Error assigning role to user {UserId}", userId);
            return StatusCode(500, "Internal server error");
            }
    }

    public class AssignRoleRequest
    {
        public RoleType RoleType { get; set; }
    }
}