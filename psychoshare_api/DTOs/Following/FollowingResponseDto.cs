namespace psychoshare_api.DTOs.Following;

public class FollowingResponseDto
{
    public long FollowingId { get; set; }
    public long UserId { get; set; }
    public long FollowedId { get; set; }
    public DateTime StartDate { get; set; }
}
