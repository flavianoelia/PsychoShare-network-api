namespace psychoshare_api.DTOs.User;

public class CreateUserRequestDTO
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
