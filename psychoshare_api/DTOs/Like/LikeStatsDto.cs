namespace psychoshare_api.DTOs.Like;

public class LikeStatsDto
{
    public long PostId { get; set; }
    public int LikeCount { get; set; }
    public bool IsLikedByCurrentUser { get; set; }
    public List<string> RecentLikerNames { get; set; } = new List<string>();
}