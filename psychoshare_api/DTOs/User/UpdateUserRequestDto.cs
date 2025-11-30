using System.ComponentModel.DataAnnotations;

namespace psychoshare_api.DTOs.User
{
    public class UpdateUserRequestDto
    {
        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(255)]
        public string? Bio { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public string? CoverPictureUrl { get; set; }
    }
}
