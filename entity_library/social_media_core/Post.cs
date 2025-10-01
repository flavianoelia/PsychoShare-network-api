public class Post
{
    public long Id { get; set; }
    
    #region Header Post
    required public long UserId { get; set; }
    required public string NameOwner { get; set; }
    required public string LastnameOwner { get; set; }
    public virtual Image? ImgOwner { get; set; }
    #endregion

    public string Description { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Authorship { get; set; } = string.Empty;
    public string Resume { get; set; } = string.Empty;
    public virtual Image? Image { get; set; }
    public virtual Pdf? Pdf { get; set; }
    public virtual List<Comment> Comments { get; set; } = new List<Comment>();
    public virtual List<Like> Likes { get; set; } = new List<Like>();

}