using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using psychoshare_api.Services.Interfaces;
using System.IO;

namespace psychoshare_api.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _env;

        public FileUploadService(IWebHostEnvironment env)// IWebHostEnvironment se inyecta para obtener la ruta de wwwroot
        {
            _env = env;
        }

        public string SaveAvatar(IFormFile file)
        {   // La lógica actual solo guarda, en el siguiente paso agregaremos validaciones
            var uploads = Path.Combine(_env.WebRootPath, "uploads", "avatars");
            Directory.CreateDirectory(uploads);

            var filePath = Path.Combine(uploads, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"/uploads/avatars/{file.FileName}";
        }
    }
}