namespace psychoshare_api.DTOs.Post;

public class FeedRequestDto
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public string? Category { get; set; }
}