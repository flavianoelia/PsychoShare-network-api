public class Ban
{   
    private long idBan;
    private long idPerson;
    private DateTime startDate;
    private DateTime endDate;
    private string reason = "";


    public long Id { get; set; }
    public long IdBan { get { return this.idBan; } set { this.idBan = value; } }
    public long IdPerson { get { return this.idPerson; } set { this.idPerson = value; } }
    public DateTime StartDate { get { return this.startDate; } set { this.startDate = value; } }
    public DateTime EndDate { get { return this.endDate; } set { this.endDate = value; } }
    public string Reason { get { return this.reason; } set { this.reason = value; } }
}