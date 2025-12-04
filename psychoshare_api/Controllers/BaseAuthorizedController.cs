using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace psychoshare_api.Controllers;

[Authorize]
public abstract class BaseAuthorizedController : ControllerBase
{
    protected bool IsAdminOrSuperAdmin()
    {
        var roleClaimValue = User.FindFirst(ClaimTypes.Role)?.Value;
        
        // El rol es numérico: "1" (User), "2" (Admin), "3" (Superadmin)
        if (long.TryParse(roleClaimValue, out long roleId))
        {
            return roleId >= 2; // Admin (2) o Superadmin (3)
        }
        
        return false;
    }

    protected long? GetAuthenticatedUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (long.TryParse(userIdClaim, out long userId))
            return userId;
        return null;
    }
}
