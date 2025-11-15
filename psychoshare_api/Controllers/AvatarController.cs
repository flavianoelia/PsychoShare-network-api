using psychoshare_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using entity_library.media;
using psychoshare_api.Services.Interfaces;
using psychoshare_api.DTOs.Media.AvatarDto;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AvatarController : ControllerBase
{
    private readonly ILogger<AvatarController> _logger;
    private readonly IFileUploadService _fileUploadService;

    public AvatarController(ILogger<AvatarController> logger, IFileUploadService fileUploadService)
    {
        _logger = logger;
        _fileUploadService = fileUploadService;
    }

    [HttpPost("upload/{userId}")]
    [Consumes("multipart/form-data")]
    public IActionResult UploadAvatar(int userId, IFormFile file)
    {
        if (_fileUploadService == null)
        {
            return StatusCode(500, "Servicio de carga no disponible.");
        }

        string fileUrl = _fileUploadService.SaveAvatar(file);

        var response = new AvatarResponseDTO
        {
            Url = fileUrl,
            FileName = file.FileName,
            ContentType = file.ContentType,
            Size = file.Length
        };

        return Ok(response);
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
