using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using psychoshare_api.Services.Interfaces;
using psychoshare_api.Configurations;
using System.IO;
using System;
using System.Linq;
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
        return SaveImage(file, FileUploadConstants.AvatarUploadPath);
    }

    public string SaveImage(IFormFile file)
    {
        return SaveImage(file, FileUploadConstants.ImageUploadPath);
    }

    private string SaveImage(IFormFile file, string relativePath)
    {
        var fileName = Path.GetFileName(file.FileName);
        var extension = Path.GetExtension(fileName).ToLowerInvariant();

        if (!FileUploadConstants.AllowedImageExtensions.Contains(extension))
            throw new Exception("Formato de imagen no permitido.");

        if (file.Length > FileUploadConstants.MaxImageSize)
            throw new Exception("El archivo excede el tamaño máximo permitido.");

        var uploads = Path.Combine(_env.WebRootPath, relativePath);
            Directory.CreateDirectory(uploads);
            var filePath = Path.Combine(uploads, fileName);

        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
        catch (Exception ex)
        {
            // Manejo de errores (puedes registrar el error o lanzar una excepción personalizada)
            throw new Exception("Error al guardar el avatar", ex);
        }

        return $"https://psychoshare_api.com/relativePath/{fileName}";
    }
}