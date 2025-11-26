namespace psychoshare_api.DTOs.Report;

public class ReportFilterDto
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Status { get; set; }
    public string? ContentType { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}