public class Report
{
    private long idReport;
    private long reporterUserId;        // Usuario que hace el reporte
    private long reportedUserId;        // Usuario reportado
    private string reason = "";         // Motivo del reporte
    private string details = "";        // Detalles adicionales
    private DateTime reportDate;        // Fecha del reporte
    private string status = "Pending";  // Pending, Under Review, Resolved, Dismissed
    private string contentType = "User"; // User, Post, Comment
    private long? contentId;            // ID del contenido reportado (opcional)

    public long IdReport { get { return this.idReport; } set { this.idReport = value; } }
    public long ReporterUserId { get { return this.reporterUserId; } set { this.reporterUserId = value; } }
    public long ReportedUserId { get { return this.reportedUserId; } set { this.reportedUserId = value; } }
    public string Reason { get { return this.reason; } set { this.reason = value; } }
    public string Details { get { return this.details; } set { this.details = value; } }
    public DateTime ReportDate { get { return this.reportDate; } set { this.reportDate = value; } }
    public string Status { get { return this.status; } set { this.status = value; } }
    public string ContentType { get { return this.contentType; } set { this.contentType = value; } }
    public long? ContentId { get { return this.contentId; } set { this.contentId = value; } }

    public void ReportUser()
    {
        // Logic for reporting a user
    }
}