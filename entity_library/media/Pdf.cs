public class Pdf : File
{
    private string title = "";
    private string url = "";
    private int idUser;

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

    public int IdUser
    {
        get { return this.idUser; }
        set { this.idUser = value; }
    }
}
