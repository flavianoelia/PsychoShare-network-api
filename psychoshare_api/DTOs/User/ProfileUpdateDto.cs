using System.ComponentModel.DataAnnotations;

namespace psychoshare_api.DTOs.User
{
    public class ProfileUpdateDto
    {
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string? Name { get; set; }

        [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string? Email { get; set; }

        [StringLength(255, ErrorMessage = "La biografía no puede superar los 255 caracteres.")]
        public string? Bio { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public string? CoverPictureUrl { get; set; }
    }
}
