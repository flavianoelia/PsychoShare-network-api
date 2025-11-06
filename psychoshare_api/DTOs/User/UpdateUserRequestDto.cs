using System.ComponentModel.DataAnnotations;

namespace psychoshare_api.DTOs.User
{
    public class UpdateUserRequestDto
    {
        [Required]
        public int idPerson { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Bio { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public string? CoverPictureUrl { get; set; }
    }
}
