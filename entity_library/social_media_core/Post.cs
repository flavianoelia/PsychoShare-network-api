public class Post
{
    public long Id { get; set; }
    public long UserId { get; set; }
    
    public string NameOwner { get; set; }
    public string imgOwner { get; set; }
    public string description { get; set; }
    public string title { get; set; }
    public string authorship { get; set; }
    public string resume {get; set;}
    public string image { get; set; }
    

    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<Like> Likes { get; set; } = new List<Like>();

}