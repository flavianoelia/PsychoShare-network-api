public class Following
{
    
    private long userId;
    private long followedUserId;
    private DateTime startDate;

    public long Id { get; set; }
    public long UserId { get { return this.userId; } set { this.userId = value; } }
    public long FollowedUserId { get { return this.followedUserId; } set { this.followedUserId = value; } }
    public DateTime StartDate { get { return this.startDate; } set { this.startDate = value; } }
}
