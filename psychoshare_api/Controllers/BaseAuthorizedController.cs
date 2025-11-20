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
        long roleId;
        if (!long.TryParse(roleClaimValue, out roleId))
            roleId = 1;
        return roleId >= 2;
    }
}
