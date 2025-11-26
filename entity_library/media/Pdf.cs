using entity_library.system;
namespace entity_library.media;
public class Pdf : BaseFile
{
    private string title = "";
    private string url = "";
    private long idUser;
    private string name = "";
    private string PdfType = "";

    public string Title
    {
        get { return this.title; }
        set { this.title = value; }
    }

    public string Url
    {
        get { return this.url; }
        set { this.url = value; }
    }

    public long IdUser
    {
        get { return this.idUser; }
        set { this.idUser = value; }
    }

    public string Name
    {
        get { return this.name; }   
        set { this.name = value; }  
    }
    public string Type
    {
        get { return this.PdfType; }
        set { this.PdfType = value; }
    }
}
