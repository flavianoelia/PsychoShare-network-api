public class Ban
{
    private long idBan;
    private long bannedUserId;
    private long bannedByAdminId;       
    private long? relatedReportId;      
    private DateTime startDate;         
    private DateTime? endDate;          
    private string reason = "";         
    private string banType = "Temporary"; 
    private bool isActive = true;       

    public long IdBan { get { return this.idBan; } set { this.idBan = value; } }
    public long BannedUserId { get { return this.bannedUserId; } set { this.bannedUserId = value; } }
    public long BannedByAdminId { get { return this.bannedByAdminId; } set { this.bannedByAdminId = value; } }
    public long? RelatedReportId { get { return this.relatedReportId; } set { this.relatedReportId = value; } }
    public DateTime StartDate { get { return this.startDate; } set { this.startDate = value; } }
    public DateTime? EndDate { get { return this.endDate; } set { this.endDate = value; } }
    public string Reason { get { return this.reason; } set { this.reason = value; } }
    public string BanType { get { return this.banType; } set { this.banType = value; } }
    public bool IsActive { get { return this.isActive; } set { this.isActive = value; } }

}