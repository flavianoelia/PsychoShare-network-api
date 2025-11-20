using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using psychoshare_api.Validations;
using psychoshare_api.Configurations;
namespace psychoshare_api.DTOs.Media.AvatarDto
{
    public class UploadAvatarDto
    {   
        [Required]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" })]
        [MaxFileSize(FileUploadConstants.MaxImageSize)]
        [AllowedMimeTypes(new[] { "image/jpeg", "image/png", "image/webp", "image/gif" })]
        public IFormFile File { get; set; } = null!;
    }
}
//Recibe un archivo (IFormFile) desde el cliente y tiene validaciones
