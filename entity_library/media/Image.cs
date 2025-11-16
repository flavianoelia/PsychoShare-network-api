using entity_library.system;
namespace entity_library.media;
public class Image : BaseFile
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
    public virtual User User { get; set; } = null!;
}
