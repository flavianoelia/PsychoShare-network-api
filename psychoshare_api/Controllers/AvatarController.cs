using psychoshare_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using entity_library.media;
using psychoshare_api.Services.Interfaces;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class AvatarController : ControllerBase
{
    private readonly ILogger<AvatarController> _logger;
    private readonly IFileUploadService _fileUploadService;

    public AvatarController(ILogger<AvatarController> logger, IFileUploadService fileUploadService)
    {
        _logger = logger;
        _fileUploadService = fileUploadService;
    }

    /*[HttpPost("upload/{userId}")]
    [Consumes("multipart/form-data")]
    public IActionResult UploadAvatar(int userId, [FromForm] IFormFile file)
    {
        if (_fileUploadService == null)
        {
            return StatusCode(500, "Servicio de carga no disponible.");
        }

        string fileUrl = _fileUploadService.SaveAvatar(file);
        return Ok(new { url = fileUrl });
    }
    */

    [HttpGet("{userId}")]
    public void GetUserAvatar(int userId)
    {
        // TODO: Get user avatar
    }

    /*[HttpPut("{userId}")]
    public void UpdateAvatar(int userId)
    {
        // TODO: Update user avatar
    }

    [HttpDelete("{userId}")]
    public void DeleteAvatar(int userId)
    {
        // TODO: Delete user avatar
    }
    */
}
