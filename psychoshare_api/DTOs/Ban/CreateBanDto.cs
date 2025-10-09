public class CreateBanDto
{
    public long BannedUserId { get; set; }
    public long BannedByAdminId { get; set; }
    public string BanType { get; set; } = "";
    public long? RelatedReportId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Reason { get; set; } = "";
}
