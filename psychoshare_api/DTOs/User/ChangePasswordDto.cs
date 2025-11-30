using System.ComponentModel.DataAnnotations;

namespace psychoshare_api.DTOs.User;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "La contraseña actual es requerida.")]
    public string OldPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "La nueva contraseña es requerida.")]
    [MinLength(8, ErrorMessage = "La nueva contraseña debe tener al menos 8 caracteres.")]
    public string NewPassword { get; set; } = string.Empty;
}
