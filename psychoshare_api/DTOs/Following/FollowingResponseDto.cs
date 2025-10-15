namespace psychoshare_api.DTOs.Following;

public class FollowingResponseDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long FollowedUserId { get; set; }
    public DateTime StartDate { get; set; }
}
