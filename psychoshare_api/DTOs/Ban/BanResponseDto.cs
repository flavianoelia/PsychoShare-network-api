public class BanResponseDto
{
    public int IdBan { get; set; }
    public int IdPerson { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; } = "";
    public bool IsActive { get; set; }
}
