public class Post
{
    public long Id { get; set; }
    public string Text { get; set; }
    public long UserId { get; set; }
    
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<Like> Likes { get; set; } = new List<Like>();

}