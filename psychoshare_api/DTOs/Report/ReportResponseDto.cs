namespace psychoshare_api.DTOs.Report;

public class ReportResponseDto
{
    public long Id { get; set; }
    public long ReporterUserId { get; set; }
    public long ReportedUserId { get; set; }
    public string Reason { get; set; } = "";
    public string Details { get; set; } = "";
    public DateTime ReportDate { get; set; }
    public string Status { get; set; } = "";
    public string ContentType { get; set; } = "";
    public long? ContentId { get; set; }
}
