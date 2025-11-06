namespace psychoshare_api.DTOs.Report;

public class CreateReportDto
{
    public long ReporterUserId { get; set; }
    public long ReportedUserId { get; set; }
    public string Reason { get; set; } = "";
    public string Details { get; set; } = "";
    public string ContentType { get; set; } = "User";
    public long? ContentId { get; set; }
}
