public class Following
{
    private int userId;
    private int followedUserId;
    private DateTime startDate;

    public int UserId { get { return this.userId; } set { this.userId = value; } }
    public int FollowedUserId { get { return this.followedUserId; } set { this.followedUserId = value; } }
    public DateTime StartDate { get { return this.startDate; } set { this.startDate = value; } }
}
