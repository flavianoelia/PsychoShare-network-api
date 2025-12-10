using psychoshare_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using entity_library.media;
using psychoshare_api.Services.Interfaces;
using dao_library.interfaces.media;
using dao_library.entity_framework.media;
using psychoshare_api.DTOs.Media.AvatarDto;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AvatarController : ControllerBase
{
    private readonly ILogger<AvatarController> _logger;
    private readonly IAvatarService _avatarService;

    public AvatarController(ILogger<AvatarController> logger, IAvatarService avatarService)
    {
        _logger = logger;
        _avatarService = avatarService;
    }

    [HttpPost("{userId}")]
    [Consumes("multipart/form-data")]
    public IActionResult UploadAvatar(long userId, [FromForm] UploadAvatarDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = _avatarService.UpsertAvatar(userId, dto.File);
            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Usuario no encontrado");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al subir/reemplazar avatar para userId={UserId}", userId);
            return StatusCode(500, "Error interno al subir/reemplazar avatar");
        }
    }
    

    [HttpGet("{userId}")]
    public IActionResult GetUserAvatar(long userId)
    {
        var response = _avatarService.GetUserAvatar(userId);
        if (response == null) return Ok("El usuario no tiene avatar");
        return Ok(response);
    }


    // Se eliminó el punto de conexión PUT: POST /api/avatar/{userId} gestiona tanto la creación como el reemplazo.

    [HttpDelete("{userId}")]
    public IActionResult DeleteAvatar(long userId)
    {
        try
        {
            _avatarService.DeleteAvatar(userId);
            return Ok("Avatar eliminado correctamente");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("El usuario no tiene avatar");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar avatar para userId={UserId}", userId);
            return StatusCode(500, "Error interno al eliminar avatar");
        }
    }
    
}
