namespace entity_library.following
{
    public class Following
    {
        public long FollowingId { get; set; }
        public long UserId { get; set; }
        public long FollowedId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
