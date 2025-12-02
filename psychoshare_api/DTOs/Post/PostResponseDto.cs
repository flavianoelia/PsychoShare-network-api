namespace psychoshare_api.DTOs.Post;

public class PostResponseDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string NameOwner { get; set; } = "";
    public string LastnameOwner { get; set; } = "";
    public string? AvatarUrl { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Authorship { get; set; } = "";
    public string Resume { get; set; } = "";
    public string? ImageUrl { get; set; }
    public string? PdfUrl { get; set; }
    public int CommentsCount { get; set; }
    public int LikesCount { get; set; }
    public string CreatedAt { get; set; } = "";
}