namespace psychoshare_api.DTOs.User;

public class UserFilterDto
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Search { get; set; }
    public string? Role { get; set; }
}