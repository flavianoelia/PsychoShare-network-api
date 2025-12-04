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
        
        // El rol ahora es string: "User", "Admin", o "Superadmin"
        return roleClaimValue == "Admin" || roleClaimValue == "Superadmin";
    }

    protected long? GetAuthenticatedUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (long.TryParse(userIdClaim, out long userId))
            return userId;
        return null;
    }
}
