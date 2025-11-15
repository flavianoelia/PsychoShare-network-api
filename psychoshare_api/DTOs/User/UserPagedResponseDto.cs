namespace psychoshare_api.DTOs.User;

public class UserPagedResponseDto
{
    public List<UserResponseDto> Users { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public bool HasMore { get; set; }
}