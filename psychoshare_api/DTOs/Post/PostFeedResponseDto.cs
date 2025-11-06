namespace psychoshare_api.DTOs.Post;

public class PostFeedResponseDto
{
    public List<PostResponseDto> Posts { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public bool HasMore { get; set; }
}