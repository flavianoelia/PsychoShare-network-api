using entity_library.system;

namespace entity_library.ReportPolicy;

public class Ban
{
    public long Id { get; set; }
    public long BannedUserId { get; set; }
    public long BannedByAdminId { get; set; }
    public long? RelatedReportId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public required string Reason { get; set; }
    public required string BanType { get; set; }
    public bool IsActive { get; set; }
    
    public long? BanUserId { get; set; }
    public virtual User? BanUser { get; set; }
}