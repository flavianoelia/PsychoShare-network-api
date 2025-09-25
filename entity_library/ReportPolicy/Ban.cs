using entity_library.system;

public class Ban
{
    public long Id { get; set; }
    public long BannedUserId { get; set; }
    public long BannedByAdminId { get; set; }
    public long? RelatedReportId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Reason { get; set; }
    public string BanType { get; set; }
    public bool IsActive { get; set; }
    public virtual User? BanUser { get; set; }
}