public class Following
{
    private long followingId;
    private long userId;
    private long followedId;
    private DateTime startDate;

    public long FollowingId { get { return this.followingId; } set { this.followingId = value; } }
    public long UserId { get { return this.userId; } set { this.userId = value; } }
    public long FollowedId { get { return this.followedId; } set { this.followedId = value; } }
    public DateTime StartDate { get { return this.startDate; } set { this.startDate = value; } }
}
