public class ReportResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string Lastname { get; set; } = "";
    public DateTime ReportDate { get; set; }
    public bool IsResolved { get; set; }
}
