using Microsoft.AspNetCore.Http;
using psychoshare_api.Configurations;
using System.ComponentModel.DataAnnotations;
using psychoshare_api.Validations;
using entity_library.media;

namespace psychoshare_api.DTOs.Media.avatarDto
{
    public class UploadAvatarDto
    {

        [Required(ErrorMessage = "El archivo es obligatorio.")]
        [MaxFileSize(FileUploadConstants.MaxImageSize)]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".webp" })]
        public IFormFile? File { get; set; }

    }
}
//Recibe un archivo (IFormFile) desde el cliente y tiene validaciones
