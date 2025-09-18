public class Image : File
{
    private string imageType = "";
    private long idUser;
    private string url = "";

    public string ImageType
    {
        get { return this.imageType; }
        set { this.imageType = value; }
    }

    public long IdUser
    {
        get { return this.idUser; }
        set { this.idUser = value; }
    }

    public string Url
    {
        get { return this.url; }
        set { this.url = value; }
    }
}
