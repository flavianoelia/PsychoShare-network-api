public class CreateBanDto
{
    public long BannedUserId { get; set; }
    // BannedByAdminId se obtiene del token JWT del usuario autenticado
    public string BanType { get; set; } = "";
    public long? RelatedReportId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Reason { get; set; } = "";
}
