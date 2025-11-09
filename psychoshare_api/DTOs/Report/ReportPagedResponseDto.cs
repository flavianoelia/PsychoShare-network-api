namespace psychoshare_api.DTOs.Report;

public class ReportPagedResponseDto
{
    public List<ReportResponseDto> Reports { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public bool HasMore { get; set; }
}