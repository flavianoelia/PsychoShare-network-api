public class Following
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long FollowedId { get; set; }
    public DateTime StartDate { get; set; }
    public virtual User? User { get; set; }
    public virtual User? FollowedUser { get; set; }
}
