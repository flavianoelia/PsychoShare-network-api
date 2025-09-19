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
    public virtual Image? Image { get; set; }
    public virtual Pdf? Pdf { get; set; }
    public virtual List<Comment> Comments { get; set; } = new List<Comment>();
    public virtual List<Like> Likes { get; set; } = new List<Like>();

}