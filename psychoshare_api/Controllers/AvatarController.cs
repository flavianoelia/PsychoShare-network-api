using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class AvatarController : ControllerBase
{
    private readonly ILogger<AvatarController> _logger;

    public AvatarController(ILogger<AvatarController> logger)
    {
        _logger = logger;
    }

    [HttpPost("upload/{userId}")]
    public void UploadAvatar(int userId)
    {
        // TODO: Upload user avatar image
    }

    [HttpGet("{userId}")]
    public void GetUserAvatar(int userId)
    {
        // TODO: Get user avatar
    }

    [HttpPut("{userId}")]
    public void UpdateAvatar(int userId)
    {
        // TODO: Update user avatar
    }

    [HttpDelete("{userId}")]
    public void DeleteAvatar(int userId)
    {
        // TODO: Delete user avatar
    }
}
