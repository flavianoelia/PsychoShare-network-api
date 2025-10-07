public class Post
{
    private long id;

    #region Header Post
    private long userId;
    private string nameOwner = "";
    private string lastnameOwner = "";
    private Image? imgOwner;

    #endregion

    #region Detail Post
    private string description = "";
    private string title = "";
    private string authorship = "";
    private string resume = "";
    private Image? image;
    private Pdf? pdf;
    #endregion

    #region Public Atributes
    public long Id
    {
        get { return this.id; }
        set { this.id = value; }
    }

    required public long UserId
    {
        get { return this.userId; }
        set { this.userId = value; }
    }
    required public string NameOwner
    {
        get { return this.nameOwner; }
        set { this.nameOwner = value; }
    }
    required public string LastnameOwner
    {
        get { return this.lastnameOwner; }
        set { this.lastnameOwner = value; }
    }
    public virtual Image? ImgOwner
    {
        get { return this.imgOwner; }
        set { this.imgOwner = value; }
    }
    public string Description
    {
        get { return this.description; }
        set { this.description = value; }
    }
    public string Title
    {
        get { return this.title; }
        set { this.title = value; }
    }
    public string Authorship
    {
        get { return this.authorship; }
        set { this.authorship = value; }
    }
    public string Resume
    {
        get { return this.resume; }
        set { this.resume = value; }
    }
    public virtual Image? Image
    {
        get { return this.image; }
        set { this.image = value; }
    }
    public virtual Pdf? Pdf
    {
        get { return this.pdf; }
        set { this.pdf = value; }
    }
    public virtual List<Comment> Comments { get; set; } = new List<Comment>();
    public virtual List<Like> Likes { get; set; } = new List<Like>();

#endregion
}
