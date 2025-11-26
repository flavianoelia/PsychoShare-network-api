namespace psychoshare_api.DTOs.Like;

public class LikeResponseDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long PostId { get; set; }
    public string UserName { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}