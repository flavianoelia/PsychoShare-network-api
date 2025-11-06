using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using psychoshare_api.Services.Interfaces;
using System.IO;
namespace psychoshare_api.Services;
public class FileUploadService : IFileUploadService
{
    private readonly IWebHostEnvironment _env;
    public FileUploadService(IWebHostEnvironment env)// IWebHostEnvironment se inyecta para obtener la ruta de wwwroot
    {
        if (env == null || string.IsNullOrEmpty(env.WebRootPath))
        throw new InvalidOperationException("WebRootPath no está disponible.");
        _env = env;
    }
    public string SaveAvatar(IFormFile file)
    {
        try
        {
            var fileName = Path.GetFileName(file.FileName);
            var uploads = Path.Combine(_env.WebRootPath, "avatars");
            Directory.CreateDirectory(uploads);
            var filePath = Path.Combine(uploads, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return $"https://psychoshare_api.com/avatars/{fileName}";
        }
        catch (Exception ex)
        {
            // Manejo de errores (puedes registrar el error o lanzar una excepción personalizada)
            throw new Exception("Error al guardar el avatar", ex);
        }
    }
}