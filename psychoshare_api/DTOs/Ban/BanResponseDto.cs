public class BanResponseDto
{
    public long IdBan { get; set; }
    public long IdPerson { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; } = "";
    public bool IsActive { get; set; }
}
