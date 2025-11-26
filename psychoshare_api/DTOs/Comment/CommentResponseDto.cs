namespace psychoshare_api.DTOs.Comment;

public class CommentResponseDto
{
    public long Id { get; set; }
    public string Text { get; set; } = "";
    public long UserId { get; set; }
    public string UserName { get; set; } = "";
    public long PostId { get; set; }
    public DateTime CreatedAt { get; set; }
}