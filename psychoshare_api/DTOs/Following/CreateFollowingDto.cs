namespace psychoshare_api.DTOs.Following;

public class CreateFollowingDto
{
    public long UserId { get; set; }
    public long FollowedId { get; set; }
}
