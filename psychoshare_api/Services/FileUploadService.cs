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
        _env = env;
    }
    public string SaveAvatar(IFormFile file)
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
}