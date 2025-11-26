namespace psychoshare_api.DTOs.User;

public class UserResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string? RoleName { get; set; }
    public string? AvatarUrl { get; set; }
}
