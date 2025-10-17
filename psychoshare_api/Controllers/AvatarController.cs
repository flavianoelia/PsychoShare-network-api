using psychoshare_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class AvatarController : ControllerBase
{
    private readonly ILogger<AvatarController> _logger;
    private readonly FileUploadService? _fileUploadService;

    public AvatarController(ILogger<AvatarController> logger, FileUploadService fileUploadService)
    {
        _logger = logger;
        _fileUploadService = fileUploadService ?? throw new ArgumentNullException(nameof(fileUploadService));
;
    }

    [HttpPost("upload/{userId}")] //El endpoint ahora recibe el archivo (IFormFile)
    public IActionResult UploadAvatar(int userId, [FromForm] IFormFile file) //ELEGAR la tarea al servicio (Separación de Responsabilidades)
    {   
        if (_fileUploadService == null)
        {
            return StatusCode(500, "Servicio de carga no disponible.");
        }
        //TODO: Validación de archivos y errores (FASE 2)
        string fileUrl = _fileUploadService.SaveAvatar(file);
        // TODO: Guardar la URL en la BD (FASE 4)
        
        return Ok(new { url = fileUrl });
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
