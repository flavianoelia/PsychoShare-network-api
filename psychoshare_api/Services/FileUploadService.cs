using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using psychoshare_api.Services.Interfaces;
using psychoshare_api.Configurations;
using System.IO;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace psychoshare_api.Services;

public class FileUploadService : IFileUploadService
{
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _http;
    private readonly ILogger<FileUploadService> _logger;

    public FileUploadService(IWebHostEnvironment env, IHttpContextAccessor http, ILogger<FileUploadService> logger)
    {
        if (env == null)
            throw new InvalidOperationException("WebRootPath no está disponible.");

        _env = env;
        _http = http;
        _logger = logger;
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
        
        var uniqueName = $"{Guid.NewGuid()}{extension}";

        var uploads = Path.Combine(_env.WebRootPath, relativePath);
        Directory.CreateDirectory(uploads);
        var filePath = Path.Combine(uploads, uniqueName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        var request = _http.HttpContext?.Request
                ?? throw new InvalidOperationException("No hay HttpContext disponible.");
        
        string baseUrl = $"{request.Scheme}://{request.Host}";
        return $"{baseUrl}/{relativePath}/{uniqueName}";
    }

    public string SavePdf(IFormFile file)
    {
        var fileName = Path.GetFileName(file.FileName);
        var extension = Path.GetExtension(fileName).ToLowerInvariant();

        if (!FileUploadConstants.AllowedPdfExtensions.Contains(extension))
            throw new Exception("Formato de PDF no permitido.");

        if (file.Length > FileUploadConstants.MaxPdfSize)
            throw new Exception("El PDF excede el tamaño máximo permitido.");

        var uniqueName = $"{Guid.NewGuid()}{extension}";

        var uploads = Path.Combine(_env.WebRootPath, FileUploadConstants.PdfUploadPath);
        Directory.CreateDirectory(uploads);

        var filePath = Path.Combine(uploads, uniqueName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        var request = _http.HttpContext?.Request
                ?? throw new InvalidOperationException("No hay HttpContext disponible.");

        string baseUrl = $"{request.Scheme}://{request.Host}";
        return $"{baseUrl}/{FileUploadConstants.PdfUploadPath}/{uniqueName}";
    }

    public void DeleteFileByUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return;

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
        {
            _logger.LogWarning("DeleteFileByUrl recibió una URL inválida: {Url}", url);
            return;
        }

        var relative = uri.AbsolutePath.TrimStart('/'); // e.g. uploads/avatars/uuid.jpg

        // Whitelist allowed upload folders
        if (!relative.StartsWith(FileUploadConstants.AvatarUploadPath, StringComparison.OrdinalIgnoreCase)
            && !relative.StartsWith(FileUploadConstants.ImageUploadPath, StringComparison.OrdinalIgnoreCase)
            && !relative.StartsWith(FileUploadConstants.PdfUploadPath, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning("DeleteFileByUrl: ruta no permitida para borrado: {Relative}", relative);
            return;
        }

        var candidate = Path.Combine(_env.WebRootPath ?? FileUploadConstants.UploadRoot, relative.Replace('/', Path.DirectorySeparatorChar));
        try
        {
            var fullWebRoot = Path.GetFullPath(_env.WebRootPath ?? FileUploadConstants.UploadRoot);
            var fullCandidate = Path.GetFullPath(candidate);

            if (!fullCandidate.StartsWith(fullWebRoot, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogWarning("DeleteFileByUrl: intento de borrar fuera de webroot. candidate={Candidate}", fullCandidate);
                return;
            }

            if (File.Exists(fullCandidate))
            {
                File.Delete(fullCandidate);
                _logger.LogInformation("Archivo eliminado: {Path}", fullCandidate);
            }
            else
            {
                _logger.LogInformation("DeleteFileByUrl: archivo no encontrado: {Path}", fullCandidate);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar archivo físico: {Url}", url);
        }
    }
}