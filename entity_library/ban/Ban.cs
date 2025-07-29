public class Ban
{
    private int idBan;
    private int idPerson;
    private DateTime startDate;
    private DateTime endDate;
    private string reason = "";

    public int IdBan { get { return this.idBan; } set { this.idBan = value; } }
    public int IdPerson { get { return this.idPerson; } set { this.idPerson = value; } }
    public DateTime StartDate { get { return this.startDate; } set { this.startDate = value; } }
    public DateTime EndDate { get { return this.endDate; } set { this.endDate = value; } }
    public string Reason { get { return this.reason; } set { this.reason = value; } }
}