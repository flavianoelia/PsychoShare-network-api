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

    public long IdBan { get; set; }
    public long BannedUserId { get; set; }
    public long BannedByAdminId { get; set; }
    public long? RelatedReportId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Reason { get; set; }
    public string BanType { get; set; }
    public bool IsActive { get; set; }
    public virtual User? User { get; set; }
}