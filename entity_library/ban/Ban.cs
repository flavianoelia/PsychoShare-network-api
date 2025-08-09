public class Ban
{
    private long idBan;
    private long bannedUserId;          // Usuario baneado
    private long bannedByAdminId;       // Admin que aplicó el ban
    private long? relatedReportId;      // Reporte que originó el ban (opcional)
    private DateTime startDate;         // Fecha de inicio del ban
    private DateTime? endDate;          // Fecha de fin (null = permanente)
    private string reason = "";         // Motivo del ban
    private string banType = "Temporary"; // Temporary, Permanent
    private bool isActive = true;       // Si el ban está activo

    public long IdBan { get { return this.idBan; } set { this.idBan = value; } }
    public long BannedUserId { get { return this.bannedUserId; } set { this.bannedUserId = value; } }
    public long BannedByAdminId { get { return this.bannedByAdminId; } set { this.bannedByAdminId = value; } }
    public long? RelatedReportId { get { return this.relatedReportId; } set { this.relatedReportId = value; } }
    public DateTime StartDate { get { return this.startDate; } set { this.startDate = value; } }
    public DateTime? EndDate { get { return this.endDate; } set { this.endDate = value; } }
    public string Reason { get { return this.reason; } set { this.reason = value; } }
    public string BanType { get { return this.banType; } set { this.banType = value; } }
    public bool IsActive { get { return this.isActive; } set { this.isActive = value; } }

    // Propiedad de conveniencia para compatibilidad con código existente
    public long IdPerson { get { return this.bannedUserId; } set { this.bannedUserId = value; } }
}