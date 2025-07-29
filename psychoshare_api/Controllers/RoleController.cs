using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    private readonly ILogger<RoleController> _logger;

    public RoleController(ILogger<RoleController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public void GetAllRoles()
    {
        // TODO: Get all available roles
    }

    [HttpGet("{id}")]
    public void GetRole(int id)
    {
        // TODO: Get specific role
    }

    [HttpPost]
    public void CreateRole()
    {
        // TODO: Create new role
    }

    [HttpPut("{id}")]
    public void UpdateRole(int id)
    {
        // TODO: Update role
    }

    [HttpDelete("{id}")]
    public void DeleteRole(int id)
    {
        // TODO: Delete role
    }
}
